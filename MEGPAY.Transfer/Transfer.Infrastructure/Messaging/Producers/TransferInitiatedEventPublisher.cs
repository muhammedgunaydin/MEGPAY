using MassTransit;
using Transfer.Application.Transfers.Interfaces;
using Transfer.Shared.Events;

namespace Transfer.Infrastructure.Messaging.Producers;

public class TransferInitiatedEventPublisher : ITransferEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    
    public TransferInitiatedEventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync(TransferInitiatedEvent @event)
    {
        await _publishEndpoint.Publish(@event);
    }
    public async Task PublishAsync(TransferCompletedEvent @event)
    {
        await _publishEndpoint.Publish(@event);
    }
}