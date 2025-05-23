using Microsoft.EntityFrameworkCore;

namespace Transfer.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.TransferAggregate.Transfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.TransferAggregate.Transfer>(builder =>
            {
                builder.ToTable("Transfers");

                builder.HasKey(a => a.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}