using ProcessHub.Repositories.Interfaces;
using ProcessHub.Entities;
using ProcessHub.Exceptions;

namespace ProcessHub.Auth
{
    public class AuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly TokenService _tokenService;

        public AuthService(IRepository<User> userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var users = await _userRepository.GetAllAsync();
            // futuramente criar um método específico para buscar por email, evitando trazer todos os usuários para a memória

            var user = users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
                throw new NotFoundException("User not found.");

            var isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isValid)
                throw new BusinessException("Invalid credentials.");

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto(token);
        }
    }
}