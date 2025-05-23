using Account.Shared.Entities;

namespace Account.Domain.AccountAggregate;

public class Account : Entity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public double CurrentBalance { get; set; }

    public Account(Guid id, string name, string surname, double currentBalance)
    {
        Id = id;
        Name = name;
        Surname = surname;
        CurrentBalance = currentBalance;
    }

    public void UpdateBalance(double currentbalance)
    {
        CurrentBalance = currentbalance;
    }
    
}
