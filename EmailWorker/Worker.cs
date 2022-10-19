using System.Text;
using System.Text.Json;
using EmailWorker.Entities;
using EmailWorker.Interfaces;
using EmailWorker.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace EmailWorker;

public class Worker : BackgroundService
{
    private readonly IEmailSender _emailSender; 
    private readonly ILogger<Worker> _logger;
    private readonly DbService _db;
    private readonly IConnection _connection;
    private IModel _channel;
    
    private const string EmailAddDepositQueue = "email_add_deposit";
    private const string Subject = "Notification";
    private const string Body = "Deposit has been created.";

    public Worker(ILogger<Worker> logger, IEmailSender emailSender, 
        DbService db, IConfiguration configuration)
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
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(EmailAddDepositQueue, exclusive: false);
        _channel.BasicQos(0, 1, false);
        _logger.LogInformation($"Queue [{EmailAddDepositQueue}] is waiting for messages.");

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (bc, ea) =>
        {
            var request  = Encoding.UTF8.GetString(ea.Body.ToArray());
            try
            {
                var email = JsonSerializer.Deserialize<string>(request);
                _emailSender.Send(email, Subject, Body);
                await _db.Add(new EmailDto {Email = email});
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (JsonException)
            {
                _logger.LogError($"JSON Parse Error: '{request}'.");
                _channel.BasicNack(ea.DeliveryTag, false, false);
            }
            catch (AlreadyClosedException)
            {
                _logger.LogInformation("RabbitMQ is closed!");
            }
            catch (Exception e)
            {
                _logger.LogError(default, e, e.Message);
            }
        };

        _channel.BasicConsume(queue: EmailAddDepositQueue, autoAck: false, consumer: consumer);

        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
        _logger.LogInformation("RabbitMQ connection is closed.");
    }
}