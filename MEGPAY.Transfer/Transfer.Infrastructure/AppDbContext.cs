using Microsoft.EntityFrameworkCore;
using Transfer.Application.Interfaces;
using Transfer.Domain.SeedWork.Entities;
using Transfer.Infrastructure.Persistence.Configurations;
using Transfer.Infrastructure.Saga;

namespace Transfer.Infrastructure
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.TransferAggregate.Transfer> Transfers { get; set; }
        public DbSet<TransferStatus> TransferStatuses { get; set; }
        public DbSet<TransferState> TransferStates { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransferStatusConfiguration());
            modelBuilder.Entity<Domain.TransferAggregate.Transfer>(builder =>
            {
                builder.ToTable("Transfers");

                builder.HasKey(a => a.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}