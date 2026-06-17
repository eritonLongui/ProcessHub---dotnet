namespace ProcessHub.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string DocumentNumber { get; private set; }


        public ICollection<Process> Processes { get; private set; }

        protected Client() { }

        public Client(string name, string email, string documentNumber)
        {
            Name = name;
            Email = email;
            DocumentNumber = documentNumber;
        }

        public void Update(string name, string email)
        {
            Name = name;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool HasActiveProcesses()
        {
            return Processes.Any(p => p.IsActive);
        }
    }
}
