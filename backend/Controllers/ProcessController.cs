using Microsoft.AspNetCore.Mvc;
using ProcessHub.Services.Interfaces;
using ProcessHub.Enums;
using ProcessHub.DTOs.Process;
using Microsoft.AspNetCore.Authorization;

namespace ProcessHub.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessService _processService;

        public ProcessController(IProcessService processService)
        {
            _processService = processService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProcessDto dto)
        {
            var result = await _processService.CreateAsync(
                dto.Title,
                dto.Description,
                dto.ClientId
            );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _processService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetByClient(Guid clientId)
        {
            var result = await _processService.GetByClientIdAsync(clientId);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] ProcessFilterDto filter)
        {
            var result = await _processService.GetPagedAsync(filter);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProcessDto dto)
        {
            await _processService.UpdateAsync(id, dto.Title, dto.Description);

            return NoContent();
        }

        [HttpPatch("{id}/assign")]
        public async Task<IActionResult> AssignUser(Guid id, Guid userId)
        {
            await _processService.AssignUserAsync(id, userId);

            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(Guid id, ProcessStatus status, Guid userId)
        {
            await _processService.ChangeStatusAsync(id, status, userId);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _processService.DeactivateAsync(id);

            return NoContent();
        }
    }
}