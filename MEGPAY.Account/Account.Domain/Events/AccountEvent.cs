namespace Account.Domain.Events;

public abstract class AccountEvent
{
    public Guid AccountId { get; set; }
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public abstract void Apply(AccountAggregate.Account account);
}