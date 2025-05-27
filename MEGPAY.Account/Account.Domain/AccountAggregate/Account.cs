using Account.Shared.Entities;

namespace Account.Domain.AccountAggregate;

public class Account : Entity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public decimal CurrentBalance { get; set; }

    public Account(Guid id, string name, string surname, decimal currentBalance)
    {
        Id = id;
        Name = name;
        Surname = surname;
        CurrentBalance = currentBalance;
    }

    public void UpdateBalance(decimal currentbalance)
    {
        CurrentBalance = currentbalance;
    }
    
}
