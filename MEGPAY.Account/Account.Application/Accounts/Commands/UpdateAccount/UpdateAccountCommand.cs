using System.Text.Json.Serialization;
using Account.Shared.Models;
using MediatR;

namespace Account.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommand : IRequest<Response<bool>>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public double Balance { get; set; } 
}