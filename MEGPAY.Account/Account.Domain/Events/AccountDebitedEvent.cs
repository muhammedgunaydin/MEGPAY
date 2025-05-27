namespace Account.Domain.Events;

public class AccountDebitedEvent : AccountEvent
{
    public decimal Amount { get; set; }

    public override void Apply(AccountAggregate.Account account)
    {
        account.CurrentBalance -=Amount;
    }
}