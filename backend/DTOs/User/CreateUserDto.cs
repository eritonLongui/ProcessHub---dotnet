using ProcessHub.Enums;

public record CreateUserDto(
    string Name,
    string Email,
    string Password,
    UserRole Role
);