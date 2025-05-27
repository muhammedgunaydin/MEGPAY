using MediatR;
using Microsoft.EntityFrameworkCore;
using Transfer.Application.Interfaces;

namespace Transfer.Application.Transfers.Queries.GetTransferStatus;

public class GetTransferStatusQueryHandler : IRequestHandler<GetTransferStatusQuery, string>
{
    private readonly IApplicationDbContext _context;

    public GetTransferStatusQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(GetTransferStatusQuery request, CancellationToken cancellationToken)
    {
        var status = await _context.TransferStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.TransferId, cancellationToken);

        return status?.Status ?? "NotFound";
    }
}