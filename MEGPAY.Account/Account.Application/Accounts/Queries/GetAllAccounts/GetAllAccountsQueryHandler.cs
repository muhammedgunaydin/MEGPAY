using System.Net;
using Account.Domain.AccountAggregate;
using Account.Shared.Models;
using MediatR;

namespace Account.Application.Accounts.Queries.GetAllAccounts;

public class GetAllAccountsQueryHandler: IRequestHandler< GetAllAccountsQuery, Response<List<Domain.AccountAggregate.Account>>>
{
    private readonly IAccountRepository _accountRepository;

    public GetAllAccountsQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Response<List<Domain.AccountAggregate.Account>>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.GetAllAsync(cancellationToken);
        return Response<List<Domain.AccountAggregate.Account>>.Success(accounts, HttpStatusCode.OK);

    }
}