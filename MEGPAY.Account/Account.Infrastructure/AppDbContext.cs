using Microsoft.EntityFrameworkCore;
using Account.Domain.AccountAggregate;

namespace Account.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.AccountAggregate.Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.AccountAggregate.Account>(builder =>
            {
                builder.ToTable("Accounts");

                builder.HasKey(a => a.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}