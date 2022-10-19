using EmailWorker;
using EmailWorker.Entities;
using EmailWorker.Interfaces;
using EmailWorker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IDbService<EmailDto>, DbService>();
    })
    .Build();

await host.RunAsync();