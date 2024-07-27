using HMS.Core.Interfaces.Messaging;
using HMS.Core.Messaging;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
namespace HMS.Infrastructure.Messaging
{
    public class MessageBusService : BackgroundService, IMessageBusService
    {
        // Em release, criar um classe no core para deixar menos sucetivel a erros de routingKey
        /*private const string Queue_Name = "prototype-hms-1";
        private const string Exchange = "prototype-hms-1";
        private const string TypeExchange = "direct";
        private const string Hostname = "localhost";*/

        private readonly IMessageBusProcessor _messageProcessor;

        public MessageBusService(IMessageBusProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => StartListener(), stoppingToken);
        }

        //Cria o canal e ja declara a Exchange
        private async static Task<IModel> ChannelFactory()
        {
            var factory = new ConnectionFactory()
            {
                HostName = MessagingSettings.Hostname
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
            return await Task.FromResult(channel);
        }


        public async Task Publish(object data, string routingKey)
        {
            var channel = await ChannelFactory();
            
            var dataSerialized = JsonSerializer.Serialize(data);
            var body = Encoding.UTF8.GetBytes(dataSerialized);
            channel.QueueBind(queue: MessagingSettings.QueueName, 
                exchange: MessagingSettings.Exchange, 
                routingKey: routingKey);

            channel.BasicPublish(exchange: MessagingSettings.Exchange,
                        routingKey: routingKey,
                        basicProperties: null,
                        body: body);

            Console.WriteLine("Published");


        }

        public async Task StartListener()
        {
            var channel = await ChannelFactory();


            channel.QueueDeclare(queue: MessagingSettings.QueueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            
            consumer.Received += (model, ea) =>
            {
                
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Message received on key: {ea.RoutingKey}");
                _messageProcessor.ProcessMessage(routingKey: ea.RoutingKey, 
                                                 bodyJson: message);
                //Console.WriteLine(message);
            };
            channel.BasicConsume(queue: MessagingSettings.QueueName,
                                autoAck: true,
                                consumer: consumer);

        }
    }
}
