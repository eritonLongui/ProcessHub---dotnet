namespace ProcessHub.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentResponseDto> CreateAsync(string fileName, string filePath, Guid processId);

        Task<DocumentResponseDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<DocumentResponseDto>> GetByProcessIdAsync(Guid processId);

        Task DeactivateAsync(Guid id);
    }
}