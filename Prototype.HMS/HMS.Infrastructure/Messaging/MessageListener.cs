﻿

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
            _channel.QueueDeclarePassive(MessagingSettings.QueueName);
            //_channel.QueueDeclare(MessagingSettings.QueueName);
            var consumer = new EventingBasicConsumer(model: _channel);
            consumer.Received += (sender, args) =>
            {
                //Console.WriteLine($"Data recieved on key: {args.RoutingKey}");
                //Console.WriteLine($"Data: {}");//
                _messageProcessor.ProcessMessage(routingKey: args.RoutingKey,
                                                message: args.Body.ToArray()).Wait();
            };
            _channel.BasicConsume(  queue: MessagingSettings.QueueName,
                                    autoAck: true,
                                    consumerTag: "", 
                                    noLocal: false, 
                                    exclusive: false,
                                    consumer: consumer);
            Console.WriteLine("Recebido?????");
        }

    }
}