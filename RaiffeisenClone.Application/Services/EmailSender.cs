using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RaiffeisenClone.Application.Interfaces;

namespace RaiffeisenClone.Application.Services;

public class EmailSender : IEmailSender, IDisposable{

    private readonly ConnectionFactory _connectionFactory;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    private const string EmailAddDepositQueue = "email_add_deposit";
    
    public EmailSender()
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = "rabbitmq"
        };
        _connectionFactory.RequestedHeartbeat = TimeSpan.FromSeconds(60);
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(EmailAddDepositQueue, exclusive: false);
    }
    public void Send(object message)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        _channel.BasicPublish(exchange: "", routingKey: EmailAddDepositQueue, body: body);
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}