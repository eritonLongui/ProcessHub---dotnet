using ProcessHub.Entities;
using ProcessHub.Repositories.Interfaces;
using ProcessHub.Services.Interfaces;
using ProcessHub.Data;
using ProcessHub.Exceptions;

namespace ProcessHub.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _documentRepository;
        private readonly IProcessRepository _processRepository;
        private readonly AppDbContext _context;

        public DocumentService(
            IRepository<Document> documentRepository,
            IProcessRepository processRepository,
            AppDbContext context)
        {
            _documentRepository = documentRepository;
            _processRepository = processRepository;
            _context = context;
        }

        public async Task<DocumentResponseDto> CreateAsync(string fileName, string filePath, Guid processId)
        {
            var process = await _processRepository.GetByIdAsync(processId);

            if (process == null)
                throw new NotFoundException("Process not found.");

            var document = new Document(fileName, filePath, processId);

            await _documentRepository.AddAsync(document);
            await _context.SaveChangesAsync();

            return MapToDto(document);
        }

        public async Task<DocumentResponseDto?> GetByIdAsync(Guid id)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null)
                throw new NotFoundException("Document not found.");

            return MapToDto(document);
        }

        public async Task<IEnumerable<DocumentResponseDto>> GetByProcessIdAsync(Guid processId)
        {
            var documents = await _documentRepository.FindAsync(d => d.ProcessId == processId);
            
            return documents.Select(MapToDto);
        }

        private static DocumentResponseDto MapToDto(Document document)
        {
            return new DocumentResponseDto(
                document.Id,
                document.FileName,
                document.FilePath,
                document.ProcessId
            );
        }

        public async Task DeactivateAsync(Guid id)
        {
            var document = await _documentRepository.GetByIdAsync(id);

            if (document == null)
                throw new NotFoundException("Document not found.");

            document.Deactivate();

            _documentRepository.Update(document);
            await _context.SaveChangesAsync();
        }
    }
}