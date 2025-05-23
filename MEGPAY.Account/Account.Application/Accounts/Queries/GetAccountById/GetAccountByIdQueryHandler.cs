using System.Net;
using Account.Domain.AccountAggregate;
using Account.Shared.Models;
using MediatR;

namespace Account.Application.Accounts.Queries.GetAccountById;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Response<Domain.AccountAggregate.Account>>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountByIdQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Response<Domain.AccountAggregate.Account>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.Id, cancellationToken);
        if (account == null)
            return Response<Domain.AccountAggregate.Account>.Fail("Account not found", HttpStatusCode.NotFound);

        return Response<Domain.AccountAggregate.Account>.Success(account, HttpStatusCode.OK);
    }
}