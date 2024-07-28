using HMS.Application.DTOs;
using HMS.Application.Mapper;
using HMS.Core.Entities;
using HMS.Core.Interfaces.Messaging;
using HMS.Core.Interfaces.Repository;
using HMS.Core.Messaging;

namespace HMS.Application.UseCases
{
    public class ClientUseCases
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMessagePublisher _messagePublisher;
        private readonly ClientMapper _mapper;
        //Implementar um response!
        public ClientUseCases(IClientRepository clientRepository, ClientMapper mapper, IMessagePublisher messagePublisher)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _messagePublisher = messagePublisher;
        }
        public async Task<bool> AddClientAsync(ClientDTO clientDTO)
        {
            Client client = _mapper.Mapper(clientDTO);
            return await _clientRepository.AddClientAsync(client);
        }

        public async Task<bool> UpdateClientAsync(ClientUpdateDTO clientDTO)
        {
            Client client = _mapper.Mapper(clientDTO);
            await Task.Delay(10);
            return await _clientRepository.UpdateClientAsync(client);
        }

        public async Task<bool> DeleteClient(long ID)
        {
            var clientDeleted = new Client(null, 0, null, null, DateTime.Now);
            clientDeleted.Id = ID;
            return await _clientRepository.DeleteClientAsync(clientDeleted);
        }
        public async Task<List<ClientDTO>> GetClientAsync()
        {
            var clients = await _clientRepository.GetClientsAsync();
            await _messagePublisher.Publish(clients, MessagingSettings.ResponseRouting);
            return
                _mapper.Mapper(clients);
        }
        public async Task<ClientDTO> GetClientByID(long ID)
        {
            return
                _mapper.Mapper(await _clientRepository.GetClientByIdAsync(ID));
        }
    }
}
