using Account.Application.Interfaces;
using Account.Domain.AccountAggregate;
using MassTransit;
using Transfer.Shared.Events;

namespace Account.API.Consumers;

public class TransferInitiatedEventConsumer : IConsumer<TransferInitiatedEvent>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public TransferInitiatedEventConsumer(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Consume(ConsumeContext<TransferInitiatedEvent> context)
    {
        var message = context.Message;

        var fromAccount = await _accountRepository.GetByIdAsync(message.FromAccountId, context.CancellationToken);
        if (fromAccount == null)
        {
            Console.WriteLine(" FromAccount not found.");
            return;
        }

        if (fromAccount.CurrentBalance < (double)message.Amount)
        {
            Console.WriteLine(" Insufficient funds.");
            return;
        }

        fromAccount.CurrentBalance -= (double)message.Amount;
        _accountRepository.Update(fromAccount);

        await _unitOfWork.SaveChangesAsync();

        Console.WriteLine($"Transferred {message.Amount} from account {message.FromAccountId}");
    }
}