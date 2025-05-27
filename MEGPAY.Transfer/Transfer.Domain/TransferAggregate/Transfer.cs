using Transfer.Shared.Entities;

namespace Transfer.Domain.TransferAggregate;

public class Transfer : Entity<Guid>
{
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
    
    public Transfer(Guid id, Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        Id = id;
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
    }
    
    public void UpdateAmount(decimal amount)
    {
        Amount = amount;
    }
}

