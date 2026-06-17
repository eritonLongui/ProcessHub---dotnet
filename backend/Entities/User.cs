using ProcessHub.Enums;

namespace ProcessHub.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public UserRole Role { get; private set; }


        public ICollection<Process> AssignedProcesses { get; private set; }

        public ICollection<ProcessHistory> ChangesMade { get; private set; }

        protected User() { }

        public User(string name, string email, string passwordHash, UserRole role)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public void Update(string name, string email)
        {
            Name = name;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}