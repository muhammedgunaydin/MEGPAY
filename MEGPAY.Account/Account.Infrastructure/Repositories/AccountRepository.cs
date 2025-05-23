using Account.Domain.AccountAggregate;

namespace Account.Infrastructure.Repositories;

public class AccountRepository : GenericRepository<Domain.AccountAggregate.Account, Guid>, IAccountRepository
{
    public AccountRepository(AppDbContext context) : base(context)
    {
    }
}