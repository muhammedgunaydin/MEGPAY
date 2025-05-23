using Account.Domain.SeedWork;

namespace Account.Domain.AccountAggregate;

public interface IAccountRepository : IGenericRepository<Account, Guid>
{
}