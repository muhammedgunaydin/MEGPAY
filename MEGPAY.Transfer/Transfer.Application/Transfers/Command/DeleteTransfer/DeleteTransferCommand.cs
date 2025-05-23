using Transfer.Shared.Models;
using MediatR;

namespace Transfer.Application.Transfers.Command.Transfer;

public class DeleteTransferCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
}