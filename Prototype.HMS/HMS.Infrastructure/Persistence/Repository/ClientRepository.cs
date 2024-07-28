using HMS.Core.Entities;
using HMS.Core.Interfaces.Repository;
using HMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HMS.Infrastructure.Persistence.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;
        public ClientRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddClientAsync(Client client)
        {
            client.CreatedAt = DateTime.Now;
            client.UpdatedAt = DateTime.Now;
            EntityEntry<Client> data = await _context.Clients.AddAsync(client);
            data.Entity.Id = 0;
            await _context.SaveChangesAsync();
            Client clientAdded = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == data.Entity.Id);
            return clientAdded is not null;
        }

        public async Task<bool> DeleteClientAsync(Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == client.Id) is null;
        }

        public async Task<Client> GetClientByIdAsync(long clientId)
        {
            return await _context.Clients
                .AsNoTracking()
                .FirstAsync(x => x.Id == clientId);
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _context.Clients
                .AsNoTracking().
                ToListAsync();
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            Client data = await _context.Clients
                .FindAsync(client.Id);
            if(data == null)
                return false;
            client.UpdatedAt = DateTime.Now;
            _context.Entry(data).CurrentValues.SetValues(client);
            _context.Entry(data)
                .Property(x => x.CreatedAt)
                .IsModified = false;

            int rowsAffected = await _context.SaveChangesAsync();
            //Console.WriteLine(_context.Entry(data).State.ToString());
            return rowsAffected == 1;
        }
    }
}
