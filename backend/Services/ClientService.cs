using ProcessHub.Entities;
using ProcessHub.Repositories.Interfaces;
using ProcessHub.Services.Interfaces;
using ProcessHub.Data;
using ProcessHub.Exceptions;

namespace ProcessHub.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly AppDbContext _context;

        public ClientService(
            IClientRepository clientRepository,
            AppDbContext context)
        {
            _clientRepository = clientRepository;
            _context = context;
        }

        public async Task<ClientResponseDto> CreateAsync(string name, string email, string documentNumber)
        {
            if (await _clientRepository.ExistsByDocumentAsync(documentNumber))
                throw new NotFoundException("Client already exists.");

            var client = new Client(name, email, documentNumber);

            await _clientRepository.AddAsync(client);
            await _context.SaveChangesAsync();

            return MapToDto(client);
        }

        public async Task UpdateAsync(Guid id, string name, string email)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
                throw new NotFoundException("Client not found.");

            client.Update(name, email);

            _clientRepository.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task<ClientResponseDto?> GetByIdAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
                return null;

            return MapToDto(client);
        }

        public async Task<IEnumerable<ClientResponseDto>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();

            return clients.Select(MapToDto);
        }

        private static ClientResponseDto MapToDto(Client client)
        {
            return new ClientResponseDto(
                client.Id,
                client.Name,
                client.Email,
                client.DocumentNumber
            );
        }

        public async Task DeactivateAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
                throw new NotFoundException("Client not found.");

            client.Deactivate();

            _clientRepository.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}