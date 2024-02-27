namespace backend.Models.DTO.ProjectService
{
    public class AddProjectServiceDTO
    {
        public required Int32 ProjectId { get; set; }
        public required DateTime PlannedDate { get; set; }
        public required string Status { get; set; }
        public Int32? ConductedBy { get; set; }
        public DateTime? ConductedDate { get; set; }
        public required string Priority { get; set; }
        public string? Description { get; set; }
        public string? ServiceReportURL { get; set; }
        public string? ServiceLevel { get; set; }
    }
}
