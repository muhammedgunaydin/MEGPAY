using MassTransit;
using Microsoft.Extensions.Logging;
using Transfer.Application.Interfaces;
using Transfer.Shared.Events;

namespace Transfer.Infrastructure.Consumers;

public class TransferCompletedEventConsumer : IConsumer<TransferCompletedEvent>
{
    private readonly ITransferStatusService _statusService;
    private readonly ILogger<TransferCompletedEventConsumer> _logger;

    public TransferCompletedEventConsumer(ITransferStatusService statusService,
        ILogger<TransferCompletedEventConsumer> logger)
    {
        _statusService = statusService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TransferCompletedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("TransferCompletedEvent received for TransferId: {TransferId}", message.TransferId);

        await _statusService.UpdateStatusAsync(message.TransferId, "Completed");
    }
}