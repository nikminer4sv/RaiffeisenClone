using Microsoft.EntityFrameworkCore;
using RaiffeisenClone.Application;
using RaiffeisenClone.Application.Services;
using RaiffeisenClone.Domain;
using RaiffeisenClone.Persistence;
using RaiffeisenClone.Persistence.Repositories;
using RaiffeisenClone.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("MSSql"));
builder.Services.AddApplication();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<DepositRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DepositService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();