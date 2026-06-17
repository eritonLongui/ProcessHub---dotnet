public record DocumentResponseDto(
    Guid Id,
    string FileName,
    string FilePath,
    Guid ProcessId
);