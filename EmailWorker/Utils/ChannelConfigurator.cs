using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace EmailWorker.Utils;

public static class ChannelConfigurator
{
    public static async Task Configure(ChannelConfiguratorRequest dto)
    {
        var consumer = new AsyncEventingBasicConsumer(dto.Channel);
        consumer.Received += async (bc, ea) =>
        {
            var request  = Encoding.UTF8.GetString(ea.Body.ToArray());
            try
            {
                var email = JsonSerializer.Deserialize<string>(request);
                dto.Function(dto.Channel, email!, ea);
            }
            catch (JsonException)
            {
                dto.Logger.LogError($"JSON Parse Error: '{request}'.");
                dto.Channel.BasicNack(ea.DeliveryTag, false, false);
            }
            catch (AlreadyClosedException)
            {
                dto.Logger.LogInformation("RabbitMQ is closed!");
            }
            catch (Exception e)
            {
                dto.Logger.LogError(default, e, e.Message);
            }
        };
        dto.Channel.BasicConsume(queue: dto.QueueName, autoAck: false, consumer: consumer);
    }

    public static IModel CreateChannel(string queueName, IConnection connection, ILogger<Worker> logger)
    {
        
        var channel = connection.CreateModel();
        channel.QueueDeclare(queueName, exclusive: false);
        channel.BasicQos(0, 1, false);
        logger.LogInformation($"Queue [{queueName}] is waiting for messages.");
        return channel;
    }
}