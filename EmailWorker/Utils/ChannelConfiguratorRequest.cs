using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailWorker.Utils;

public class ChannelConfiguratorRequest
{
    public IModel Channel { get; set; }
    
    public string QueueName { get; set; }

    public ILogger<Worker> Logger { get; set; }
    
    public Action<IModel, string, BasicDeliverEventArgs> Function { get; set; }
}