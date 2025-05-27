using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transfer.Shared.Events;
using Account.Domain.Events;
using Account.Application.Interfaces;

namespace Account.Infrastructure.Consumers;

public class TransferStartedEventConsumer : IConsumer<TransferStartedEvent>
{
    private readonly AppDbContext _context;
    private readonly IAccountEventStore _eventStore;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<TransferStartedEventConsumer> _logger;

    public TransferStartedEventConsumer(
        AppDbContext context,
        IAccountEventStore eventStore,
        IPublishEndpoint publishEndpoint,
        ILogger<TransferStartedEventConsumer> logger)
    {
        _context = context;
        _eventStore = eventStore;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TransferStartedEvent> context)
    {
        var message = context.Message;

        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == message.FromAccountId);

        if (account is null || account.CurrentBalance < message.Amount)
        {
            _logger.LogWarning("Transfer failed. Account not found or insufficient balance.");

            await _publishEndpoint.Publish(new TransferFailedEvent
            {
                TransferId = message.TransferId
            });

            return;
        }
        
        account.CurrentBalance -= message.Amount;
        await _context.SaveChangesAsync();
        
        var debitEvent = new AccountDebitedEvent
        {
            AccountId = account.Id,
            Amount = message.Amount
        };

        await _eventStore.AppendEventAsync(debitEvent, context.CancellationToken);
        
        await _publishEndpoint.Publish(new TransferCompletedEvent
        {
            TransferId = message.TransferId,
            ToAccountId = message.ToAccountId,
            Amount = message.Amount
        });

        _logger.LogInformation("Transfer completed and AccountDebitedEvent stored.");
    }
}
