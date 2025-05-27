using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Account.Application.Interfaces;
using Account.Domain.Events;
using Transfer.Shared.Events;

namespace Account.Infrastructure.Consumers;

public class TransferCompletedEventConsumer : IConsumer<TransferCompletedEvent>
{
    private readonly AppDbContext _context;
    private readonly IAccountEventStore _eventStore;
    private readonly ILogger<TransferCompletedEventConsumer> _logger;

    public TransferCompletedEventConsumer(
        AppDbContext context,
        IAccountEventStore eventStore,
        ILogger<TransferCompletedEventConsumer> logger)
    {
        _context = context;
        _eventStore = eventStore;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TransferCompletedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("TransferCompletedEvent received for TransferId: {TransferId}", message.TransferId);

        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == message.ToAccountId);
        if (account == null)
        {
            _logger.LogWarning("Target account not found: {AccountId}", message.ToAccountId);
            return;
        }
        
        account.CurrentBalance += message.Amount;
        await _context.SaveChangesAsync();

        var creditEvent = new AccountCreditedEvent
        {
            AccountId = account.Id,
            Amount = message.Amount
        };

        await _eventStore.AppendEventAsync(creditEvent, context.CancellationToken);

        _logger.LogInformation("AccountCreditedEvent stored for AccountId: {AccountId}", account.Id);
    }
}