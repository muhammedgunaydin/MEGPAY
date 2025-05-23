using Account.Shared.Models;
using MediatR;

namespace Account.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommand : IRequest<Response<bool>>
{
    public Guid Id { get; set; }
}