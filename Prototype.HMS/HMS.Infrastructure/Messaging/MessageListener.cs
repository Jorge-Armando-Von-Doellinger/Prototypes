

using HMS.Core.Interfaces.Messaging;
using HMS.Core.Messaging;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HMS.Infrastructure.Messaging
{
    public class MessageListener : BackgroundService, IMessageListener
    {
        private readonly IModel _channel;
        private readonly IMessageBusProcessor _messageProcessor;
        public MessageListener(ChannelFactory channelFactory, IMessageBusProcessor messageProcessor) 
        { 
            _channel = channelFactory.ChannelFactoryRabbitMQ();
            _messageProcessor = messageProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => { Task.Delay(1000); StartListener(); }, stoppingToken);
            
            //ExecuteAsync(stoppingToken);
        }

        public async Task StartListener()
        {
            string queue = "prototype-hms-1";
            string exchange = "prototype-hms-1";
            string post = "client.post";
            //_channel.QueueDeclarePassive(MessagingSettings.QueueName);
            //_channel.QueueDeclare(MessagingSettings.QueueName);
            _channel.ExchangeDeclare(exchange, "direct");

            _channel.QueueDeclare(queue,
                false,
                false,
                false,
                null);
            _channel.QueueBind(queue,
                exchange,
                post);

            _channel.ExchangeBind(destination: exchange,
                source: exchange,
                routingKey: post);
            var consumer = new EventingBasicConsumer(model: _channel);
            consumer.Received += (sender, args) =>
            {
                Console.WriteLine($"Data recieved on key: {args.RoutingKey}");
                //Console.WriteLine($"Data: {}");

                _messageProcessor.ProcessMessage(routingKey: args.RoutingKey,
                                                message: args.Body.ToArray()).Wait();
            };
            _channel.BasicConsume(  queue: MessagingSettings.QueueName,
                                    autoAck: true,
                                    consumerTag: "", 
                                    noLocal: false, 
                                    exclusive: false,
                                    arguments: null,
                                    consumer: consumer);
            Console.WriteLine("Recebido?????");
        }

    }
}
