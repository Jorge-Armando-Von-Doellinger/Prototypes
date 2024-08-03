using Gateway.Core.Configs;
using Gateway.Core.Entity;
using Gateway.Core.Interfaces.Messaging;
using Gateway.Infrastructure.Services;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Gateway.Infrastructure.Messaging
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IModel _channel;
        public MessagePublisher(ChannelFactory channelFactory) 
        {
            _channel = channelFactory.RabbitChannel();
        }
        public async Task Publish(MessageEntity message)
        {
            try
            {
                string messages = JsonSerializer.Serialize(message.Data);
                messages = new JsonManipulationService().FixCapitalizeJson(messages);
                Console.WriteLine(messages);
                byte[] body = Encoding.UTF8.GetBytes(messages);
                _channel.BasicPublish(exchange: ClientMessagingSettings.Exchange,
                                     routingKey: ClientMessagingSettings.PostClientRouting,
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
                
                await Task.CompletedTask;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
