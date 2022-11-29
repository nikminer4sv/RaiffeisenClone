using Identity.Domain;
using Identity.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(builder);
    }
}