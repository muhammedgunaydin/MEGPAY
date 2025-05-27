using Microsoft.EntityFrameworkCore;
using Transfer.Application.Interfaces;
using Transfer.Domain.SeedWork.Entities;

namespace Transfer.Infrastructure.Services;

public class TransferStatusService : ITransferStatusService
{
    private readonly AppDbContext _context;

    public TransferStatusService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(Guid transferId, string status)
    {
        var entity = new TransferStatus
        {
            Id = transferId,
            Status = status,
            CreatedAt = DateTime.UtcNow
        };

        _context.TransferStatuses.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(Guid transferId, string status)
    {
        var existing = await _context.TransferStatuses.FirstOrDefaultAsync(x => x.Id == transferId);
        if (existing is null) return;

        existing.Status = status;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}