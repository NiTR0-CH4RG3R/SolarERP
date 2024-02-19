namespace backend.Models.DTO.ProjectTest
{
    public class AddProjectTestDTO
    {
        public required string Name { get; set; }
        public required Int32 Passed { get; set; }
        public Int32? ConductedBy { get; set; }
        public DateTime? ConductedDate { get; set; }
        public string? Comments { get; set; }
    }
}
