using Account.Domain.Events;

namespace Account.Application.Interfaces;

public interface IAccountEventStore
{
    Task AppendEventAsync(AccountEvent @event, CancellationToken cancellationToken);
}