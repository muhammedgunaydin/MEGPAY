using System.Text.Json.Serialization;
using Transfer.Shared.Models;
using MediatR;

namespace Transfer.Application.Transfers.Command.UpdateTransfer;

public class UpdateTransferCommand : IRequest<Response<bool>>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public decimal Amount { get; set; } 
}