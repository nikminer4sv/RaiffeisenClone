using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaiffeisenClone.Domain;

namespace RaiffeisenClone.Persistence.EntityTypeConfigurations;

public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
{
    public void Configure(EntityTypeBuilder<Avatar> builder)
    {
        builder.HasKey(a => a.Id);
    }
}