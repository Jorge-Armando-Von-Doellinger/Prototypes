using HMS.Application.DTOs;
using HMS.Application.Mapper;
using HMS.Core.Interfaces.Messaging;
using HMS.Core.Interfaces.Repository;
using HMS.Core.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace HMS.Application.UseCases
{
    public class MessageBusProcessor : IMessageBusProcessor
    { 
        private readonly IServiceScopeFactory _scopeFactory;
        public MessageBusProcessor(IServiceScopeFactory serviceScopeFactory) 
        {
            _scopeFactory = serviceScopeFactory;
        }
        public async Task ProcessMessage(string routingKey, string bodyJson)
        {
            if(routingKey != null)
            {
                ClientDTO desserialized = JsonSerializer.Deserialize<ClientDTO>(bodyJson);
                using(var scope = _scopeFactory.CreateScope())
                {
                    ClientUseCases scopedService = scope.ServiceProvider
                                    .GetRequiredService<ClientUseCases>();

                    Console.WriteLine(JsonSerializer.Serialize(desserialized));
                    if(routingKey == MessagingSettings.PostClientRouting)
                    {
                        bool addedSuccess = await scopedService.AddClientAsync(desserialized);
                        if(addedSuccess)
                            Console.WriteLine("Adicionado com sucesso");
                        return;
                    }
                    if(routingKey == MessagingSettings.PutClientRouting)
                    {
                        ClientUpdateDTO bodyUpdate = JsonSerializer.Deserialize<ClientUpdateDTO>(bodyJson);
                        bool putSucess = await scopedService.UpdateClientAsync(bodyUpdate);
                        if(putSucess)
                            Console.WriteLine("Put Sucess");
                        return;
                    }
                        

                    Console.WriteLine("Add Client Not Succesfull");
                    return;
                }
            }
            Console.WriteLine("routingKey have value nullable");
            return;
        }
    }
}
