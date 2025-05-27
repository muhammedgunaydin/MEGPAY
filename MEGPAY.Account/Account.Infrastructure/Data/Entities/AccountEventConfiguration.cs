using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Infrastructure.Data.Entities;

public class AccountEventConfiguration : IEntityTypeConfiguration<AccountEventEntity>
{
    public void Configure(EntityTypeBuilder<AccountEventEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.AccountId, x.Version }).IsUnique();
        builder.Property(x => x.Data).HasColumnType("jsonb");
    }
}