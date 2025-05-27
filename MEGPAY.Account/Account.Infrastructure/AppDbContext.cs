using Microsoft.EntityFrameworkCore;
using Account.Domain.AccountAggregate;
using Account.Infrastructure.Data.Entities;

namespace Account.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.AccountAggregate.Account> Accounts { get; set; }
        public DbSet<AccountEventEntity> AccountEvents { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountEventConfiguration());
            modelBuilder.Entity<Domain.AccountAggregate.Account>(builder =>
            {
                builder.ToTable("Accounts");

                builder.HasKey(a => a.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}