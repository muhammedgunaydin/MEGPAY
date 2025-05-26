namespace Transfer.Shared.Entities;

public abstract class Entity<TId> : IEntity<TId>
{
    public virtual TId Id { get; set; }
    public DateTime CreatedOn { get; set; }
}