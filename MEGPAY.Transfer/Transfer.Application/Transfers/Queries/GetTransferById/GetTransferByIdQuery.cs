using Transfer.Shared.Models;
using MediatR;

namespace Transfer.Application.Transfers.Queries.GetTransferById;

public class GetTransferByIdQuery : IRequest<Response<Domain.TransferAggregate.Transfer>>
{
    public Guid Id { get; set; }
}