using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName).HasMaxLength(32).IsRequired();
        builder.Property(u => u.FirstName).HasMaxLength(32).IsRequired();
        builder.Property(u => u.DateOfBirth).IsRequired();
    }
}