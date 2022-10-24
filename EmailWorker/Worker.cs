using EmailWorker.Entities;
using EmailWorker.Interfaces;
using EmailWorker.Utils;
using RabbitMQ.Client;

namespace EmailWorker;

public class Worker : BackgroundService
{
    private readonly IEmailSender _emailSender; 
    private readonly ILogger<Worker> _logger;
    private readonly IDbService<EmailDto> _db;
    private readonly IConnection _connection;
    private IModel _addDepositChannel;
    private IModel _deleteDepositChannel;
    
    public Worker(ILogger<Worker> logger, IEmailSender emailSender, 
        IDbService<EmailDto> db, IConfiguration configuration)
    {
        _logger = logger;
        _emailSender = emailSender;
        _db = db;
        
        var connectionFactory = new ConnectionFactory
        {
            HostName = configuration.GetConnectionString("rabbitmq"),
            DispatchConsumersAsync = true,
        };
        _connection = connectionFactory.CreateConnection();
    } 
        

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _deleteDepositChannel = ChannelConfigurator.CreateChannel("email_delete_deposit", _connection, _logger);
        _addDepositChannel = ChannelConfigurator.CreateChannel("email_add_deposit", _connection, _logger);
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        await ChannelConfigurator.Configure(new ChannelConfiguratorRequest
        {
            Channel = _addDepositChannel,
            QueueName = "email_add_deposit",
            Logger = _logger,
            Function = async (channel, email, ea) =>
            {
                _emailSender.Send(email, "Raiffeisen Notification", "Deposit has been created");
                await _db.Add(new EmailDto {Email = email});
                channel.BasicAck(ea.DeliveryTag, false);
            }
        });
        
        await ChannelConfigurator.Configure(new ChannelConfiguratorRequest
        {
            Channel = _deleteDepositChannel,
            QueueName = "email_delete_deposit",
            Logger = _logger,
            Function = async (channel, email, ea) =>
            {
                _emailSender.Send(email, "Raiffeisen Notification", "Deposit has been deleted");
                await _db.Add(new EmailDto {Email = email + "|delete"});
                channel.BasicAck(ea.DeliveryTag, false);
            }
        });

        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
        _logger.LogInformation("RabbitMQ connection is closed.");
    }
}