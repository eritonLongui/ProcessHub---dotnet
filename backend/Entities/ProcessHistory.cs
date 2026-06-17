using ProcessHub.Enums;

namespace ProcessHub.Entities
{
    public class ProcessHistory : BaseEntity
    {
        public Guid ProcessId { get; private set; }
        public Process Process { get; private set; }

        public ProcessStatus OldStatus { get; private set; }

        public ProcessStatus NewStatus { get; private set; }

        public Guid ChangedByUserId { get; private set; }
        public User ChangedByUser { get; private set; }
        
        public DateTime ChangedAt { get; private set; }

        protected ProcessHistory() { }

        public ProcessHistory(
            Guid processId,
            ProcessStatus oldStatus,
            ProcessStatus newStatus,
            Guid changedByUserId)
        {
            ProcessId = processId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
            ChangedByUserId = changedByUserId;
            ChangedAt = DateTime.UtcNow;
        }
    }
}