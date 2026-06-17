using ProcessHub.Enums;

namespace ProcessHub.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateAsync(string name, string email, string passwordHash, UserRole role);

        Task UpdateAsync(Guid id, string name, string email);

        Task ChangePasswordAsync(Guid id, string newPasswordHash);
        
        Task<UserResponseDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<UserResponseDto>> GetAllAsync();

        Task DeactivateAsync(Guid id);
    }
}