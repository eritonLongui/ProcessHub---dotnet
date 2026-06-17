using ProcessHub.Entities;
using ProcessHub.Enums;

namespace ProcessHub.Repositories.Interfaces
{
    public interface IProcessRepository : IRepository<Process>
    {
        Task<Process?> GetWithDetailsAsync(Guid id);

        Task<IEnumerable<Process>> GetByClientIdAsync(Guid clientId);

        Task<IEnumerable<Process>> GetByAssignedUserIdAsync(Guid userId);

        Task<IEnumerable<Process>> SearchByTitleAsync(string title);

        Task<IEnumerable<Process>> SearchByStatusAsync(ProcessStatus status);
    }
}
