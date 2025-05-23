namespace Transfer.Shared.Events;

public class TransferCompletedEvent
{
    public Guid TransferId { get; set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
}