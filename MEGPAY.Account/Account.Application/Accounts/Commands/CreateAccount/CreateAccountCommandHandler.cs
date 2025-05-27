using System.Net;
using Account.Domain.AccountAggregate;
using Account.Domain.Events;
using Account.Domain.SeedWork;
using Account.Shared.Models;
using MediatR;
using Account.Application.Interfaces;

namespace Account.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Response<bool>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IDbContextHandler _dbContextHandler;
    private readonly IAccountEventStore _eventStore;

    public CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        IDbContextHandler dbContextHandler,
        IAccountEventStore eventStore)
    {
        _accountRepository = accountRepository;
        _dbContextHandler = dbContextHandler;
        _eventStore = eventStore;
    }

    public async Task<Response<bool>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new Domain.AccountAggregate.Account(Guid.NewGuid(), request.Name, request.Surname, request.Balance);

        await _accountRepository.SaveAsync(account, cancellationToken);
        await _dbContextHandler.SaveChangesAsync(cancellationToken);

        var @event = new AccountCreatedEvent
        {
            AccountId = account.Id,
            Name = account.Name,
            Surname = account.Surname,
            InitialBalance = account.CurrentBalance
        };

        await _eventStore.AppendEventAsync(@event, cancellationToken);

        return Response<bool>.Success(true, HttpStatusCode.Created);
    }
}