using Gateway.Core.Configs;
using Gateway.Core.Entity;
using RabbitMQ.Client;

namespace Gateway.Infrastructure.Messaging
{
    public static class ChannelDeclare
    {
        public static IModel Declare(IModel channel, MessageEntity message)
        {
            channel.ExchangeDeclare(exchange: ClientMessagingSettings.Exchange,
                                    type: ClientMessagingSettings.TypeExchange);
            //channel.ExchangeBind(message.Exchange, null, message.RoutingKey);

            channel.QueueDeclare(queue: ClientMessagingSettings.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.QueueBind(queue: ClientMessagingSettings.QueueName,
                              exchange: ClientMessagingSettings.Exchange,
                              routingKey: ClientMessagingSettings.PostClientRouting,
                              arguments: null);
            return channel;
        }
    }
}
