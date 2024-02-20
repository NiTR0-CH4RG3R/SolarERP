namespace backend.Models.DTO.ProjectResource 
{
    public class GetProjectResourceDTO : AddProjectResourceDTO 
    { 
        public Int32? Id { get; set; }
        public Int32? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
    }
}
