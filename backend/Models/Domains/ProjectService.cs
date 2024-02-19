namespace backend.Models.Domains
{
    public class ProjectService
    {

        public enum ProjectServiceStatus
        {
            Pending, Done
        }
        public enum ProjectServicePriority
        {
            Urgent, High, Normal, Low
        }

        public Int32? Id { get; set; }
        public required Int32 ProjectId { get; set; }
        public required DateTime PlannedDate { get; set; }
        public required string Status { get; set; }
        public Int32? ConductedBy { get; set; }
        public DateTime? ConductedDate { get; set; }
        public required string Priority { get; set; }
        public string? Description { get; set; }
        public string? ServiceReportURL { get; set; }
        public string? ServiceLevel { get; set; }
        public Int32? LastUpdatedBy { get; set; }
        public required DateTime LastUpdatedDateTime { get; set; }

    }
}