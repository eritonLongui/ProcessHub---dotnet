public record ProcessResponseDto(
    Guid Id,
    string Title,
    string Description,
    string Status,
    ClientSummaryDto Client
);