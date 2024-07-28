using HMS.Core.Interfaces.Messaging;
using HMS.Core.Messaging;
using RabbitMQ.Client;

namespace HMS.Infrastructure.Messaging
{
    public class ChannelFactory
    {
        public IModel ChannelFactoryRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: MessagingSettings.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            channel.ExchangeDeclare(exchange: MessagingSettings.Exchange,
                                    type: MessagingSettings.TypeExchange);
            return channel;
        }
    }
}
