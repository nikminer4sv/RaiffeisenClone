using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RaiffeisenClone.Application.Interfaces;

namespace RaiffeisenClone.Application.Services;

public class EmailSender : IEmailSender, IDisposable{

    private readonly ConnectionFactory _connectionFactory;
    private readonly IConnection _connection;
    private readonly IModel _addDepositChannel;
    private readonly IModel _deleteDepositChannel;
    private readonly IDictionary<string, IModel> _queueToChannel;

    public EmailSender()
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = "rabbitmq"
        };
        _connectionFactory.RequestedHeartbeat = TimeSpan.FromSeconds(60);
        _connection = _connectionFactory.CreateConnection();
        _queueToChannel = new Dictionary<string, IModel>();
        
        _addDepositChannel = _connection.CreateModel();
        _addDepositChannel.QueueDeclare("email_add_deposit", exclusive: false);
        
        _deleteDepositChannel = _connection.CreateModel();
        _deleteDepositChannel.QueueDeclare("email_delete_deposit", exclusive: false);

        _queueToChannel["email_add_deposit"] = _addDepositChannel;
        _queueToChannel["email_delete_deposit"] = _deleteDepositChannel;
    }
    public void Send(object message, string queue)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        _queueToChannel[queue].BasicPublish(exchange: "", routingKey: queue, body: body);
    }

    public void Dispose()
    {
        _addDepositChannel.Dispose();
        _deleteDepositChannel.Dispose();
        _connection.Dispose();
    }
}