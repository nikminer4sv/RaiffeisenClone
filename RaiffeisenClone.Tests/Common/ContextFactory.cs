using Microsoft.EntityFrameworkCore;
using RaiffeisenClone.Persistence;

namespace RaiffeisenClone.Tests.Common;

public static class ContextFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=localhost;Database=Raiffeisen;User=sa;Password=7514895263a-B")
            .Options;
        
        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    public static void DeleteData(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }
}