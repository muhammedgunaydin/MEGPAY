namespace Transfer.Domain.SeedWork.Entities;

public class TransferStatus
{
    public Guid Id { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}