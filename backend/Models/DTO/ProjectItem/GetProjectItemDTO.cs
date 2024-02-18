namespace backend.Models.DTO.ProjectItem {
	public class GetProjectItemDTO : AddProjectItemDTO {
		public Int32? Id { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
