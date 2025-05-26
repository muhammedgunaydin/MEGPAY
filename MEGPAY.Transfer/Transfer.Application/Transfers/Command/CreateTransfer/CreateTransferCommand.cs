using MediatR;
using Transfer.Shared.Models;

namespace Transfer.Application.Transfers.Command.CreateTransfer;

public class CreateTransferCommand : IRequest<Response<bool>>
{
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public double Amount { get; set; }
}