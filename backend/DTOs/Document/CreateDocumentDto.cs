public record CreateDocumentDto(
    string FileName,
    string FilePath,
    Guid ProcessId
);