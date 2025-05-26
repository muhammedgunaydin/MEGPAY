using System.Net;
using Transfer.Domain.TransferAggregate;
using Transfer.Domain.SeedWork;
using Transfer.Shared.Models;
using MediatR;
using Transfer.Application.Transfers.Command.Transfer;

namespace Transfer.Application.Transfers.Command.DeleteTransfer;

public class DeleteTransferCommandHandler : IRequestHandler<DeleteTransferCommand, Response<bool>>
{
    private readonly ITransferRepository _transferRepository;
    private readonly IDbContextHandler _dbContextHandler;

    public DeleteTransferCommandHandler(ITransferRepository transferRepository, IDbContextHandler dbContextHandler)
    {
        _transferRepository = transferRepository;
        _dbContextHandler = dbContextHandler;
    } 
    
    public async Task<Response<bool>> Handle(DeleteTransferCommand request, CancellationToken cancellationToken)
    {
        var transfer = await _transferRepository.GetByIdAsync(request.Id, cancellationToken);
        if (transfer == null)
        {
            return Response<bool>.Fail("Transfer not found!", HttpStatusCode.NotFound);
        }
        _transferRepository.Remove(transfer);
        await _dbContextHandler.SaveChangesAsync(cancellationToken);
        return Response<bool>.Success(true, HttpStatusCode.OK);
    }
}