namespace Transfer.Shared.Entities;

public interface IEntity<TId>
{
    TId Id { get; set; }
    public DateTime CreatedOn { get; set; }
}