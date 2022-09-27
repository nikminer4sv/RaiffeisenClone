using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Persistence.EntityTypeConfigurations;

public class DepositConfiguration : IEntityTypeConfiguration<Deposit>
{
    public void Configure(EntityTypeBuilder<Deposit> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.UserId).IsRequired();
        builder.Property(d => d.Term).IsRequired();
        builder.Property(d => d.Currency).HasMaxLength(2).IsRequired();
        builder.Property(d => d.Bid).IsRequired();
        builder.Property(d => d.IsReplenished).IsRequired();
        builder.Property(d => d.IsWithdrawed).IsRequired();
    }
}