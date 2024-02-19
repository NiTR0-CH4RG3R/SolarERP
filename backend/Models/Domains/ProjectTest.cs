namespace backend.Models.Domains
{
    public class ProjectTest
    {
        public Int32? Id { get; set; }
        public required Int32 ProjectId { get; set; }
        public required string Name { get; set; }
        public required Int32 Passed { get; set; }
        public Int32? ConductedBy { get; set; }
        public DateTime? ConductedDate { get; set; }
        public string? Comments { get; set; }
        public Int32? LastUpdatedBy { get; set; }
        public required DateTime LastUpdatedDateTime { get; set; }
    }
}
