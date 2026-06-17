using ProcessHub.Entities;
using ProcessHub.Enums;
using ProcessHub.Repositories.Interfaces;
using ProcessHub.Services.Interfaces;
using ProcessHub.Data;
using ProcessHub.Exceptions;

namespace ProcessHub.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly AppDbContext _context;

        public UserService(
            IRepository<User> userRepository,
            AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<UserResponseDto> CreateAsync(string name, string email, string password, UserRole role)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User(name, email, passwordHash, role);

            await _userRepository.AddAsync(user);
            await _context.SaveChangesAsync();

            return MapToDto(user);
        }

        public async Task UpdateAsync(Guid id, string name, string email)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException("User not found.");

            user.Update(name, email);

            _userRepository.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(Guid id, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException("User not found.");

            var newHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            user.ChangePassword(newHash);

            _userRepository.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserResponseDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException("User not found.");

            return MapToDto(user);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(MapToDto);
        }

        private static UserResponseDto MapToDto(User user)
        {
            return new UserResponseDto(
                user.Id,
                user.Name,
                user.Email
            );
        }

        public async Task DeactivateAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException("User not found.");

            user.Deactivate();

            _userRepository.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}