using Microsoft.AspNetCore.Mvc;
using ProcessHub.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ProcessHub.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var result = await _userService.CreateAsync(
                dto.Name,
                dto.Email,
                dto.Password,
                dto.Role
            );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
        {
            await _userService.UpdateAsync(id, dto.Name, dto.Email);

            return NoContent();
        }

        [HttpPatch("{id}/password")]
        public async Task<IActionResult> ChangePassword(Guid id, string newPassword)
        {
            await _userService.ChangePasswordAsync(id, newPassword);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _userService.DeactivateAsync(id);

            return NoContent();
        }
    }
}