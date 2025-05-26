using Transfer.Domain.TransferAggregate;

namespace Transfer.Infrastructure.Repositories;

public class TransferRepository : GenericRepository<Domain.TransferAggregate.Transfer, Guid>, ITransferRepository
{
    public TransferRepository(AppDbContext context) : base(context)
    {
    }
}