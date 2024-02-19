namespace backend.Models.DTO.ProjectResource
{
    public class AddProjectResourceDTO
    {
        public required Int32 ProjectId { get; set; }
        public required String Category { get; set; }
        public required String URL { get; set; }
        public String? Comments { get; set; }
        
    }
}
