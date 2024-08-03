using Gateway.Core.Configs;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace Gateway.Infrastructure.Messaging
{
    public class ChannelFactory
    {
        public ChannelFactory()
        {

        }

        public IModel RabbitChannel()
        {
            try
            {
                string queue = "prototype-hms-1";
                string exchange = "prototype-hms-1";
                string post = "client.post";
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    Port = 5672
                };
                IConnection connection = factory.CreateConnection();
                IModel channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange, "direct");

                channel.QueueDeclare(queue,
                    false,
                    false,
                    false,
                    null);
                channel.QueueBind(queue,
                    exchange,
                    post);

                channel.ExchangeBind(destination: exchange,
                source: exchange,
                    routingKey: post);
                //channel.ExchangeBind(message.Exchange, null, message.RoutingKey);


                return channel;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
