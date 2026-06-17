using ProcessHub.Enums;

namespace ProcessHub.Entities
{
    public class Process : BaseEntity
    {
        public string Title { get; private set; }

        public string Description { get; private set; }

        public ProcessStatus Status { get; private set; }


        public Guid ClientId { get; private set; }
        public Client Client { get; private set; }
        

        public Guid? AssignedUserId { get; private set; }
        public User? AssignedUser { get; private set; }


        public ICollection<Document> Documents { get; private set; }

        public ICollection<ProcessHistory> History { get; private set; }

        protected Process()
        {
            Documents = new List<Document>();
            History = new List<ProcessHistory>();
        }

        public Process(string title, string description, Guid clientId)
        {
            Title = title;
            Description = description;
            ClientId = clientId;
            Status = ProcessStatus.Open;

            Documents = new List<Document>();
            History = new List<ProcessHistory>();
        }

        public void Update(string title, string description)
        {
            Title = title;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignUser(Guid userId)
        {
            AssignedUserId = userId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeStatus(ProcessStatus newStatus, Guid changedByUserId)
        {
            if (Status == newStatus)
                return;
            
            var oldStatus = Status;

            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;

            var history = new ProcessHistory( Id, oldStatus, newStatus, changedByUserId);
            
            History.Add(history);
        }

        // adcionar documentos

        public bool CanBeCompleted()
        {
            return Status == ProcessStatus.Approved;
        }
    }
}