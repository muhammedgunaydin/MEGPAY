namespace Transfer.Shared.Events;

public class TransferFailedEvent
{
    public Guid TransferId { get; set; }
}