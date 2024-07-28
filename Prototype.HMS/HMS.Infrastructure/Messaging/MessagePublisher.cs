using HMS.Core.Interfaces.Messaging;
using HMS.Core.Messaging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HMS.Infrastructure.Messaging
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IModel _channel;

        public MessagePublisher(ChannelFactory channelFactory)
        {
            _channel = channelFactory.ChannelFactoryRabbitMQ();
        }
        public Task Publish(object data, string routingKey)
        {
            
            string dataJson = JsonSerializer.Serialize(data);
            byte[] body = Encoding.UTF8.GetBytes(dataJson);
            //var teste = Encoding.Default.GetString(body);
            _channel.QueueBind(queue: MessagingSettings.QueueName,
                                exchange: MessagingSettings.Exchange,
                                routingKey: routingKey,
                                arguments: null);
            _channel.BasicPublish(exchange: MessagingSettings.Exchange, 
                                routingKey: routingKey,
                                basicProperties: null, 
                                body: body);
            //await Task.Delay(1000);
            Console.WriteLine("Enviado...");
            //_channel.Close();
            return Task.FromResult(_channel);
        }
    }
}
