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
        private readonly IMessageBusService _messageBusService;
        public ClientController(ClientUseCases application, IMessageBusService messageBusService) 
        {
            _application = application;
            _messageBusService = messageBusService;
        }
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await _application.GetClientAsync());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetClientById(long ID)
        {
            var client = await _application.GetClientByID(ID);
            if(client != null)
                return Ok(client);
            return BadRequest("Usuario não encontrado!");
        }
        [HttpPost]
        public async Task<IActionResult> AddClient(ClientDTO newClient)
        {
            await _messageBusService.Publish(newClient, MessagingSettings.PostClientRouting);
            return Ok("");
        }
        [HttpPost("teste")]
        public async Task<IActionResult> AddClientMessage(ClientDTO newClient)
        {
            
            var serialized = JsonSerializer.Serialize(newClient);
            _messageBusService.Publish(newClient, MessagingSettings.PostClientRouting);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientUpdateDTO client)
        {
            if(client == null)
                return BadRequest("Dados invalidos!");
            await _messageBusService.Publish(client, MessagingSettings.PutClientRouting);
            return Accepted();
            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(long ID)
        {
            if(await _application.DeleteClient(ID))
                return Accepted();
            return BadRequest("Ocorreu um erro durante a exclusão!");
        }
    }
}
