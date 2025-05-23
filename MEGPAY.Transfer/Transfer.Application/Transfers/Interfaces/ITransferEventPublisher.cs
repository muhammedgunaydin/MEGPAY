using Transfer.Shared.Events;

namespace Transfer.Application.Transfers.Interfaces;

public interface ITransferEventPublisher
{
    Task PublishAsync(TransferInitiatedEvent @event);
    Task PublishAsync(TransferCompletedEvent @event);
}