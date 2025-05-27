using MediatR;
using Transfer.Shared.Models;

namespace Transfer.Application.Transfers.Command.CreateTransfer;

public class CreateTransferCommand : IRequest<Response<bool>>
{
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public string ClientRequestId { get; set; }
}