using System.Net;
using Transfer.Domain.TransferAggregate;
using Transfer.Shared.Models;
using MediatR;

namespace Transfer.Application.Transfers.Queries.GetTransferById;

public class GetTransferByIdQueryHandler : IRequestHandler<GetTransferByIdQuery, Response<Domain.TransferAggregate.Transfer>>
{
    private readonly ITransferRepository _transferRepository;

    public GetTransferByIdQueryHandler(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }

    public async Task<Response<Domain.TransferAggregate.Transfer>> Handle(GetTransferByIdQuery request, CancellationToken cancellationToken)
    {
        var transfer = await _transferRepository.GetByIdAsync(request.Id, cancellationToken);
        if (transfer == null)
            return Response<Domain.TransferAggregate.Transfer>.Fail("Transfer not found", HttpStatusCode.NotFound);

        return Response<Domain.TransferAggregate.Transfer>.Success(transfer, HttpStatusCode.OK);
    }
}