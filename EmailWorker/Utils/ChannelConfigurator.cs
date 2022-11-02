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
                try
                {
                    var email = JsonSerializer.Deserialize<string>(request);
                    dto.Function(dto.Channel, email!, ea);
                    dto.Channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    dto.Channel.BasicReject(ea.DeliveryTag, false);
                }
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
        
        channel.ExchangeDeclare($"{queueName}-dead-exchange", ExchangeType.Fanout);
        
        var queueArgs = new Dictionary<string, object>();
        queueArgs["x-dead-letter-exchange"] = $"{queueName}-dead-exchange";
        channel.QueueDeclare(queueName, exclusive: false, arguments: queueArgs);
        
        var deadLetterQueueArgs = new Dictionary<string, object>();
        deadLetterQueueArgs["x-dead-letter-exchange"] = "amq.direct";
        deadLetterQueueArgs["x-dead-letter-routing-key"] = queueName;
        deadLetterQueueArgs["x-message-ttl"] = 30000;
        channel.QueueDeclare($"{queueName}-dead-letter-queue", exclusive: false, arguments: deadLetterQueueArgs);
        
        channel.QueueBind(queueName, exchange: "amq.direct", routingKey: queueName);
        channel.QueueBind($"{queueName}-dead-letter-queue", exchange: $"{queueName}-dead-exchange", routingKey: "");
        
        logger.LogInformation($"Queue [{queueName}] is waiting for messages.");
        return channel;
    }
}