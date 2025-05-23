using Transfer.Shared.Models;
using MediatR;

namespace Transfer.Application.Transfers.Queries.GetAllTransfers;

public class GetAllTransfersQuery : IRequest<Response<List<Domain.TransferAggregate.Transfer>>>
{
}