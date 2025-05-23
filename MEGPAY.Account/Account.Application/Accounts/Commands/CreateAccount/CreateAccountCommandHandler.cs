using System.Net;
using Account.Domain.AccountAggregate;
using Account.Domain.SeedWork;
using Account.Shared.Models;
using MediatR;

namespace Account.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Response<bool>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IDbContextHandler _dbContextHandler;

    public CreateAccountCommandHandler(IAccountRepository accountRepository, IDbContextHandler dbContextHandler)
    {
        _accountRepository = accountRepository;
        _dbContextHandler = dbContextHandler;
    }

    public async Task<Response<bool>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account =
            new Domain.AccountAggregate.Account(Guid.NewGuid(), request.Name, request.Surname, request.Balance);
        await _accountRepository.SaveAsync(account, cancellationToken);
        await _dbContextHandler.SaveChangesAsync(cancellationToken);
        return Response<bool>.Success(true, HttpStatusCode.Created);
    }
}