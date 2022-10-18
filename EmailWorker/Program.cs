using EmailWorker;
using EmailWorker.Interfaces;
using EmailWorker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<DbService>();
    })
    .Build();

await host.RunAsync();