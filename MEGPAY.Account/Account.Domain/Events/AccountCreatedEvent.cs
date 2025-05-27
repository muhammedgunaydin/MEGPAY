namespace Account.Domain.Events;

public class AccountCreatedEvent : AccountEvent
{
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public decimal InitialBalance { get; set; }
    
    public override void Apply(AccountAggregate.Account account)
    {
        throw new NotImplementedException();
    }
}