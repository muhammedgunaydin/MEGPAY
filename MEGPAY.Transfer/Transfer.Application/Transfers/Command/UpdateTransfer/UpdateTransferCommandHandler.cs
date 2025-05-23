using System.Net;
using Transfer.Domain.TransferAggregate;
using Transfer.Domain.SeedWork;
using Transfer.Shared.Models;
using MediatR;

namespace Transfer.Application.Transfers.Command.UpdateTransfer;

public class UpdateTransferCommandHandler : IRequestHandler<UpdateTransferCommand, Response<bool>>
{
    private readonly ITransferRepository _transferRepository;
    private readonly IDbContextHandler _dbContextHandler;
    
    public UpdateTransferCommandHandler(ITransferRepository transferRepository, IDbContextHandler dbContextHandler)
    {
        _transferRepository = transferRepository;
        _dbContextHandler = dbContextHandler;
    }
    
    public async Task<Response<bool>> Handle(UpdateTransferCommand request, CancellationToken cancellationToken)
    {
        var transfer = await _transferRepository.GetByIdAsync(request.Id, cancellationToken);
        if (transfer == null)
        {
            return Response<bool>.Fail("Transfer not found!", HttpStatusCode.NotFound);
        }
        transfer.UpdateAmount(request.Amount);
        await _dbContextHandler.SaveChangesAsync(cancellationToken);
        return Response<bool>.Success(true, HttpStatusCode.OK);
    }
}