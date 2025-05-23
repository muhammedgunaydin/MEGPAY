using Account.Application.Interfaces;
using Account.Domain.AccountAggregate;
using MassTransit;
using Transfer.Shared.Events;

namespace Account.API.Consumers;

public class TransferCompletedEventConsumer : IConsumer<TransferCompletedEvent>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TransferCompletedEventConsumer(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<TransferCompletedEvent> context)
    {
        var message = context.Message;

        var toAccount = await _accountRepository.GetByIdAsync(message.ToAccountId, context.CancellationToken);
        if (toAccount == null)
        {
            Console.WriteLine(" ToAccount not found.");
            return;
        }

        toAccount.CurrentBalance += (double)message.Amount;
        _accountRepository.Update(toAccount);

        await _unitOfWork.SaveChangesAsync();

        Console.WriteLine($"Deposited {message.Amount} to account {message.ToAccountId}");
    }
}