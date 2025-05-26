using Transfer.Domain.SeedWork;

namespace Transfer.Domain.TransferAggregate;

public interface ITransferRepository : IGenericRepository<Transfer, Guid>
{
    
}