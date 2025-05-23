using System.Net;
using Account.Domain.AccountAggregate;
using Account.Domain.SeedWork;
using Account.Shared.Models;
using MediatR;

namespace Account.Application.Accounts.Commands.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Response<bool>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IDbContextHandler _dbContextHandler;

    public DeleteAccountCommandHandler(IAccountRepository accountRepository, IDbContextHandler dbContextHandler)
    {
        _accountRepository = accountRepository;
        _dbContextHandler = dbContextHandler;
    } 
    
    public async Task<Response<bool>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.Id, cancellationToken);
        if (account == null)
        {
            return Response<bool>.Fail("Account not found!", HttpStatusCode.NotFound);
        }
        _accountRepository.Remove(account);
        await _dbContextHandler.SaveChangesAsync(cancellationToken);
        return Response<bool>.Success(true, HttpStatusCode.OK);
    }
}