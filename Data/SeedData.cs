using ProcessHub.Entities;
using ProcessHub.Enums;
using Microsoft.EntityFrameworkCore;

namespace ProcessHub.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            // Garante que banco está criado
            await context.Database.MigrateAsync();

            // Verifica se já existem usuários
            if (await context.Users.AnyAsync())
                return;

            // Criar senha padrão
            var adminPassword = BCrypt.Net.BCrypt.HashPassword("Admin123!");
            var operatorPassword = BCrypt.Net.BCrypt.HashPassword("Operator123!");

            // Admin
            var admin = new User(
                "Admin",
                "admin@processhub.com",
                adminPassword,
                UserRole.Admin
            );

            // Operator
            var operatorUser = new User(
                "Operator",
                "operator@processhub.com",
                operatorPassword,
                UserRole.Operator
            );

            context.Users.AddRange(admin, operatorUser);

            await context.SaveChangesAsync();
        }
    }
}