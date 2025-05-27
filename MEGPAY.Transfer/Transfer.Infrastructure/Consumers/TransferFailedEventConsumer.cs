using MassTransit;
using Microsoft.Extensions.Logging;
using Transfer.Application.Interfaces;
using Transfer.Shared.Events;

namespace Transfer.Infrastructure.Consumers;

public class TransferFailedEventConsumer : IConsumer<TransferFailedEvent>
{
    private readonly ITransferStatusService _statusService;
    private readonly ILogger<TransferFailedEventConsumer> _logger;

    public TransferFailedEventConsumer(ITransferStatusService statusService, ILogger<TransferFailedEventConsumer> logger)
    {
        _statusService = statusService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TransferFailedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("TransferFailedEvent received for TransferId: {TransferId}", message.TransferId);

        await _statusService.UpdateStatusAsync(message.TransferId, "Failed");
    }
}