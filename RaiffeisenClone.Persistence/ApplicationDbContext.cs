using Microsoft.EntityFrameworkCore;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence.EntityTypeConfigurations;

namespace RaiffeisenClone.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Deposit> Deposits { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new DepositConfiguration());
        base.OnModelCreating(builder);
    }
}