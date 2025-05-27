namespace Account.Domain.Events;

public class AccountCreditedEvent : AccountEvent
{
    public decimal Amount { get; set; }

    public override void Apply(AccountAggregate.Account account)
    {
        account.CurrentBalance += Amount;
    }
}