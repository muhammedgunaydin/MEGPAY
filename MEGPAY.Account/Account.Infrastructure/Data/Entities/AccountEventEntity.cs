namespace Account.Infrastructure.Data.Entities;

public class AccountEventEntity
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public int Version { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
