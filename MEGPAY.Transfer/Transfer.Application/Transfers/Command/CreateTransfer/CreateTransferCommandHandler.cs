using System.Net;
using MediatR;
using Transfer.Domain.SeedWork;
using Transfer.Domain.TransferAggregate;
using Transfer.Shared.Models;

namespace Transfer.Application.Transfers.Command.CreateTransfer;

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, Response<bool>>
{
    private readonly ITransferRepository _transferRepository;
    private readonly IDbContextHandler _dbContextHandler;

    public CreateTransferCommandHandler(
        ITransferRepository transferRepository,
        IDbContextHandler dbContextHandler)
    {
        _transferRepository = transferRepository;
        _dbContextHandler = dbContextHandler;
    }

    public async Task<Response<bool>> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var transfer = new Domain.TransferAggregate.Transfer(Guid.NewGuid(), request.FromAccountId, request.ToAccountId, request.Amount);
        await _transferRepository.SaveAsync(transfer, cancellationToken);
        await _dbContextHandler.SaveChangesAsync(cancellationToken);
        return Response<bool>.Success(true, HttpStatusCode.Created);
    }
}