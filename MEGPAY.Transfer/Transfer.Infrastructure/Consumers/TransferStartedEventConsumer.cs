using MassTransit;
using Microsoft.Extensions.Logging;
using Transfer.Application.Interfaces;
using Transfer.Shared.Events;

namespace Transfer.Infrastructure.Consumers;

public class TransferStartedEventConsumer : IConsumer<TransferStartedEvent>
{
    private readonly ITransferStatusService _statusService;
    private readonly ILogger<TransferStartedEventConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    

    public TransferStartedEventConsumer(ITransferStatusService statusService, ILogger<TransferStartedEventConsumer> logger,IPublishEndpoint publishEndpoint)
    {
        _statusService = statusService;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<TransferStartedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("TransferStartedEvent: TransferId={TransferId}, FromAccountId={From}, ToAccountId={To}, Amount={Amount}", 
            message.TransferId, message.FromAccountId, message.ToAccountId, message.Amount);

        await _statusService.CreateAsync(message.TransferId, "Pending");

        _logger.LogInformation("TransferCompletedEvent published for TransferId: {TransferId}", message.TransferId);
    }
}