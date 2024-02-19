namespace backend.Models.DTO.ProjectTest
{
    public class GetProjectTestDTO : AddProjectTestDTO
    {
        public Int32? Id { get; set; }
        public required Int32 ProjectId { get; set; }
        public Int32? LastUpdatedBy { get; set; }
        public required DateTime LastUpdatedDateTime { get; set; }
    }
}
