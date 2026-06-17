using ProcessHub.Enums;
using ProcessHub.DTOs.Common;
using ProcessHub.DTOs.Process;

namespace ProcessHub.Services.Interfaces
{
    public interface IProcessService
    {
        Task<ProcessResponseDto> CreateAsync(string title, string description, Guid clientId);

        Task UpdateAsync(Guid id, string title, string description);
    
        Task AssignUserAsync(Guid processId, Guid userId);

        Task ChangeStatusAsync(Guid processId, ProcessStatus newStatus, Guid UserId);

        Task<ProcessResponseDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<ProcessResponseDto>> GetByClientIdAsync(Guid clientId);

        Task DeactivateAsync(Guid id);

        Task<PagedResult<ProcessResponseDto>> GetPagedAsync(ProcessFilterDto filter);
    }
}
