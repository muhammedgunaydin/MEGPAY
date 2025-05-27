namespace Transfer.Shared.Events;
public class TransferStartedEvent
{
    public Guid TransferId { get; set; }
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public string ClientRequestId { get; set; } = string.Empty;
}