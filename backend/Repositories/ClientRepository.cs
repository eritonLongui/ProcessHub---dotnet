using Microsoft.EntityFrameworkCore;
using ProcessHub.Data;
using ProcessHub.Entities;
using ProcessHub.Repositories.Interfaces;

namespace ProcessHub.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByDocumentAsync(string documentNumber)
        {
            return await _context.Clients
                .AnyAsync(c => c.DocumentNumber == documentNumber);
        }
    }
}
