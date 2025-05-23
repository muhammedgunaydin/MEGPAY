using Account.Shared.Models;
using MediatR;

namespace Account.Application.Accounts.Queries.GetAllAccounts;

public class GetAllAccountsQuery : IRequest<Response<List<Domain.AccountAggregate.Account>>>
{
}