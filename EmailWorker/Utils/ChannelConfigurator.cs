using System.Text;
using System.Text.Json;
using EmailWorker.Entities;
using EmailWorker.Interfaces;
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
}