using MediatR;

namespace Transfer.Application.Transfers.Queries.GetTransferStatus;

public class GetTransferStatusQuery : IRequest<string>
{
    public Guid TransferId { get; set; }

    public GetTransferStatusQuery(Guid transferId)
    {
        TransferId = transferId;
    }
}