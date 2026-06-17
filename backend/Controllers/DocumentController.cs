using Microsoft.AspNetCore.Mvc;
using ProcessHub.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ProcessHub.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDocumentDto dto)
        {
            var result = await _documentService.CreateAsync(
                dto.FileName,
                dto.FilePath,
                dto.ProcessId
            );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _documentService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("process/{processId}")]
        public async Task<IActionResult> GetByProcess(Guid processId)
        {
            var result = await _documentService.GetByProcessIdAsync(processId);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _documentService.DeactivateAsync(id);

            return NoContent();
        }
    }
}