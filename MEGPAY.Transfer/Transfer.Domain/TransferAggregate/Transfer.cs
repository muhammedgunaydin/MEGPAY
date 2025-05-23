using Transfer.Shared.Entities;

namespace Transfer.Domain.TransferAggregate;

public class Transfer : Entity<Guid>
{
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public double Amount { get; set; }
    
    public Transfer(Guid id, Guid fromAccountId, Guid toAccountId, double amount)
    {
        Id = id;
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
    }
    
    public void UpdateAmount(double amount)
    {
        Amount = amount;
    }
}

