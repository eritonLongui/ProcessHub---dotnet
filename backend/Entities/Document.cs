namespace ProcessHub.Entities
{
    public class Document : BaseEntity
    {
        public string FileName { get; private set; }

        public string FilePath { get; private set; }

        public Guid ProcessId { get; private set; }

        public Process Process { get; private set; }

        protected Document() { }

        public Document(string fileName, string filePath, Guid processId)
        {
            FileName = fileName;
            FilePath = filePath;
            ProcessId = processId;
        }

        public void Update(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}