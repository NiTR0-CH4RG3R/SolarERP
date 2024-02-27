namespace backend.Models.DTO.ProjectService
{
    public class GetProjectServiceDTO : AddProjectServiceDTO
    {
        public Int32? Id { get; set; }
        public Int32? LastUpdatedBy { get; set; }
        public required DateTime LastUpdatedDateTime { get; set; }
    }
}
