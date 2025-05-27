using Microsoft.EntityFrameworkCore;
using Transfer.Domain.SeedWork.Entities;

namespace Transfer.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TransferStatus> TransferStatuses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}