using System.Net;
using Account.Domain.AccountAggregate;
using Account.Domain.SeedWork;
using Account.Shared.Models;
using MediatR;

namespace Account.Application.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Response<bool>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IDbContextHandler _dbContextHandler;
    
    public UpdateAccountCommandHandler(IAccountRepository accountRepository, IDbContextHandler dbContextHandler)
    {
        _accountRepository = accountRepository;
        _dbContextHandler = dbContextHandler;
    }
    
    public async Task<Response<bool>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.Id, cancellationToken);
        if (account == null)
        {
            return Response<bool>.Fail("Account not found!", HttpStatusCode.NotFound);
        }
        account.UpdateBalance(request.Balance);
        await _dbContextHandler.SaveChangesAsync(cancellationToken);
        return Response<bool>.Success(true, HttpStatusCode.OK);
    }
}