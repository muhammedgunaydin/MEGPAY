using Account.Shared.Models;
using MediatR;

namespace Account.Application.Accounts.Queries.GetAccountById;

public class GetAccountByIdQuery : IRequest<Response<Domain.AccountAggregate.Account>>
{
    public Guid Id { get; set; }
}