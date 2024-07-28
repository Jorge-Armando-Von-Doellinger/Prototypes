using HMS.Application.DTOs;
using HMS.Application.Mapper;
using HMS.Core.Interfaces.Messaging;
using HMS.Core.Interfaces.Repository;
using HMS.Core.Messaging;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;

namespace HMS.Application.UseCases
{
    public class MessageBusProcessor : IMessageBusProcessor
    { 
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMessagePublisher _messagePublisher;
        public MessageBusProcessor(IServiceScopeFactory serviceScopeFactory, IMessagePublisher messagePublisher) 
        {
            _messagePublisher = messagePublisher;
            _scopeFactory = serviceScopeFactory;
        }
        public async Task ProcessMessage(string routingKey, byte[] message)
        {
            if(routingKey != null)
            {
                string bodyJson = Encoding.UTF8.GetString(message);
                using(var scope = _scopeFactory.CreateScope())
                {
                    ClientUseCases scopedService = scope.ServiceProvider
                                    .GetRequiredService<ClientUseCases>();

                    if(routingKey == MessagingSettings.PostClientRouting)
                    {
                        
                        ClientDTO bodyPost = JsonSerializer.Deserialize<ClientDTO>(bodyJson);
                        await Task.FromResult(bodyJson);
                        bool addedSuccess = await scopedService.AddClientAsync(bodyPost);
                        if(!addedSuccess)
                            Console.WriteLine("Error on add client");
                        return;
                    }
                    else if(routingKey == MessagingSettings.PutClientRouting)
                    {
                        ClientUpdateDTO bodyUpdate = JsonSerializer.Deserialize<ClientUpdateDTO>(bodyJson);
                        await Task.FromResult(bodyUpdate);
                        bool putSucess = await scopedService.UpdateClientAsync(bodyUpdate);
                        if(!putSucess)
                            Console.WriteLine("Update NOT sucessfull");
                        return;
                    }
                    else if(routingKey == MessagingSettings.DeleteClientRouting)
                    {
                        long bodyDelete = JsonSerializer.Deserialize<long>(bodyJson);
                        await Task.FromResult(bodyDelete);
                        if(bodyDelete > 0)
                        {
                            if(await scopedService.DeleteClient(bodyDelete))
                                Console.WriteLine("Deletado com sucesso!");
                            return;
                        }
                        else
                            Console.WriteLine("Erro ao remover cliente");
                    }
                    else if(routingKey == MessagingSettings.GetClientRouting)
                    {
                        await _messagePublisher.Publish(
                            data: await scopedService.GetClientAsync(), 
                            routingKey: MessagingSettings.ResponseRouting);

                        return;
                    }
                    else if(routingKey == MessagingSettings.ResponseRouting)
                    {
                        Console.WriteLine(bodyJson);
                        return;
                    }
                    else if(routingKey == MessagingSettings.GetClientByIDRouting)
                    {

                        long ID = long.Parse(Encoding.UTF8.GetString(message));

                        await _messagePublisher.Publish(
                                                data: await scopedService.GetClientByID(ID),
                                                routingKey: MessagingSettings.ResponseRouting);
                    } else
                    {
                        Console.WriteLine("Incorrect routingKey");
                    }
                    Console.WriteLine($"Message Recieved on key: {routingKey}");

                    return;
                }
            } else
                Console.WriteLine("batata");

            Console.WriteLine("routingKey have value nullable");
            return;
        }
    }
}
