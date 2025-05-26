using Account.Shared.Models;
using MediatR;

namespace Account.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommand : IRequest<Response<bool>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public double Balance { get; set; }
}