namespace ProcessHub.Services.Interfaces
{
    public interface IClientService
    {
        Task<ClientResponseDto> CreateAsync(string name, string email, string documentNumber);

        Task UpdateAsync(Guid id, string name, string email);

        Task<ClientResponseDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<ClientResponseDto>> GetAllAsync();

        Task DeactivateAsync(Guid id);
    }
}