namespace Transfer.Domain.SeedWork;

public interface IDbContextHandler
{
    Task SaveChangesAsync(CancellationToken cancellationToken=default);
}