using HMS.Application.DTOs;
using HMS.Application.UseCases;
using HMS.Core.Entities;
using HMS.Core.Interfaces.Messaging;
using HMS.Core.Messaging;
using HMS.Infrastructure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientUseCases _application;
        private readonly IMessagePublisher _messagePublisher;
        public ClientController(ClientUseCases application, IMessagePublisher messagePubliser) 
        {
            _application = application;
            _messagePublisher = messagePubliser;
        }
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            await _messagePublisher.Publish(new Object(), MessagingSettings.GetClientRouting);
            return Ok();
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetClientById(long ID)
        {
            await _messagePublisher.Publish(data: ID, 
                    routingKey: MessagingSettings.GetClientByIDRouting);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddClient(ClientDTO newClient)
        {
            await _messagePublisher.Publish(newClient, "client.post");
            return Ok("");
        }
        /*[HttpPost("teste")]
        public async Task<IActionResult> AddClientMessage(ClientDTO newClient)
        {
            
            var serialized = JsonSerializer.Serialize(newClient);
            await _messageBusService.Publish(newClient, MessagingSettings.PostClientRouting);
            return Ok();
        }*/

        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientUpdateDTO client)
        {
            if(client == null)
                return BadRequest("Dados invalidos!");
            await _messagePublisher.Publish(client, MessagingSettings.PutClientRouting);
            return Accepted();
            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(long ID)
        {
            if(ID > 0)
            {
                await _messagePublisher.Publish(ID, MessagingSettings.DeleteClientRouting);
                return Accepted();
            }
            return BadRequest("Ocorreu um erro durante a exclusão!");
        }
    }
}
