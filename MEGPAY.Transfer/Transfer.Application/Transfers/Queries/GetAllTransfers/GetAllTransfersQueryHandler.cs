using System.Net;
using Transfer.Domain.TransferAggregate;
using Transfer.Shared.Models;
using MediatR;

namespace Transfer.Application.Transfers.Queries.GetAllTransfers;

public class GetAllTransfersQueryHandler: IRequestHandler< GetAllTransfersQuery, Response<List<Domain.TransferAggregate.Transfer>>>
{
    private readonly ITransferRepository _transferRepository;

    public GetAllTransfersQueryHandler(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }

    public async Task<Response<List<Domain.TransferAggregate.Transfer>>> Handle(GetAllTransfersQuery request, CancellationToken cancellationToken)
    {
        var transfers = await _transferRepository.GetAllAsync(cancellationToken);
        return Response<List<Domain.TransferAggregate.Transfer>>.Success(transfers, HttpStatusCode.OK);

    }
}