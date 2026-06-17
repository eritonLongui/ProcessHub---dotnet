using Microsoft.EntityFrameworkCore;
using ProcessHub.Data;
using ProcessHub.Entities;
using ProcessHub.Repositories.Interfaces;
using ProcessHub.Enums;

namespace ProcessHub.Repositories
{
    public class ProcessRepository : Repository<Process>, IProcessRepository
    {
        public ProcessRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Process?> GetWithDetailsAsync(Guid id)
        {
            return await _context.Processes
                .Include(p => p.Client)
                .Include(p => p.AssignedUser)
                .Include(p => p.Documents)
                .Include(p => p.History)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Process>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Processes
                .Where(p => p.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Process>> GetByAssignedUserIdAsync(Guid userId)
        {
            return await _context.Processes
                .Where(p => p.AssignedUserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Process>> SearchByTitleAsync(string title)
        {
            return await _context.Processes
                .Where(p => p.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<IEnumerable<Process>> SearchByStatusAsync(ProcessStatus status)
        {
            return await _context.Processes
                .Where(p => p.Status == status)
                .ToListAsync();
        }
    }
}
