using System.Text;
using System.Text.Json;
using EmailWorker.Entities;
using EmailWorker.Interfaces;
using EmailWorker.Services;
using EmailWorker.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

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
        _deleteDepositChannel = _connection.CreateModel();
        _deleteDepositChannel.QueueDeclare("email_delete_deposit", exclusive: false);
        _deleteDepositChannel.BasicQos(0, 1, false);
        _logger.LogInformation($"Queue [email_delete_deposit] is waiting for messages.");
        
        _addDepositChannel = _connection.CreateModel();
        _addDepositChannel.QueueDeclare("email_add_deposit", exclusive: false);
        _addDepositChannel.BasicQos(0, 1, false);
        _logger.LogInformation($"Queue [email_add_deposit] is waiting for messages.");

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