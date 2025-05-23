using Account.Domain.SeedWork;

namespace Account.Infrastructure.Repositories;

public class DbContextHandler(AppDbContext dbContext) : IDbContextHandler
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes.", e);
        }
    }
}