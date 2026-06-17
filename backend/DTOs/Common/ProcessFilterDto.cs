using ProcessHub.Enums;

namespace ProcessHub.DTOs.Process
{
    public class ProcessFilterDto
    {
        public string? Title { get; set; }
        public ProcessStatus? Status { get; set; }
        public Guid? ClientId { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}