using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transfer.Domain.SeedWork.Entities;

namespace Transfer.Infrastructure.Persistence.Configurations;

public class TransferStatusConfiguration : IEntityTypeConfiguration<TransferStatus>
{
    public void Configure(EntityTypeBuilder<TransferStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}