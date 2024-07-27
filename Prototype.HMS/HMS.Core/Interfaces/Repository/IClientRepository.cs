using HMS.Core.Entities;

namespace HMS.Core.Interfaces.Repository
{
    public interface IClientRepository
    {
        Task<List<Client>> GetClientsAsync();
        Task<Client> GetClientByIdAsync(long clientId);
        Task<bool> AddClientAsync(Client client);
        Task<bool> UpdateClientAsync(Client client);
        Task<bool> DeleteClientAsync(Client client);
    }
}
