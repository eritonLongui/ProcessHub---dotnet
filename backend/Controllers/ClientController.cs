using Microsoft.AspNetCore.Mvc;
using ProcessHub.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ProcessHub.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            var result = await _clientService.CreateAsync(
                dto.Name,
                dto.Email,
                dto.DocumentNumber
            );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _clientService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _clientService.GetAllAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateClientDto dto)
        {
            await _clientService.UpdateAsync(id, dto.Name, dto.Email);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _clientService.DeactivateAsync(id);

            return NoContent();
        }
    }
}