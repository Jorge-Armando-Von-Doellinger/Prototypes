using HMS.Application.DTOs;
using HMS.Core.Entities;
using HMS.Core.Messaging;

namespace HMS.Application.Mapper
{
    public class ClientMapper
    {
        public Client Mapper(ClientDTO clientDTO)
        {
            return new Client
            (
                clientDTO.Name,
                clientDTO.CPF,
                clientDTO.Email,
                clientDTO.PhoneNumber,
                clientDTO.DateBirth
            );
        }

        public List<Client> Mappper(List<ClientDTO> clientDTOs)
        {
            List<Client> clients = new ();
            foreach(ClientDTO clientDTO in clientDTOs) 
            { 
                clients.Add(Mapper(clientDTO)); 
            }
            return clients;
        }

        public ClientDTO Mapper(Client client)
        {
            return new ClientDTO
            {
                Name = client.Name,
                CPF = client.CPF,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                DateBirth = client.DateBirth
            };
        }
        public List<ClientDTO> Mapper(List<Client> clients)
        {
            var clientDTO = new List<ClientDTO>();
            foreach(Client client in clients)
            {
                clientDTO.Add(Mapper(client));
            }
            return clientDTO;
        }
        public Client Mapper(ClientUpdateDTO client)
        {   
            return new Client
            {
                Id = client.ID,
                Name = client.Name,
                CPF = client.CPF,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                DateBirth = client.DateBirth
            };
        }
    }
}
