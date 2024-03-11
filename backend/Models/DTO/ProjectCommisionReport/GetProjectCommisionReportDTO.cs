namespace backend.Models.DTO.ProjectCommisionReport {
	public class GetProjectCommisionReportDTO : AddProjectCommisionReportDTO {
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
