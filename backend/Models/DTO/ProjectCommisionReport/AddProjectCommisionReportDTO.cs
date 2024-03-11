namespace backend.Models.DTO.ProjectCommisionReport {
	public class AddProjectCommisionReportDTO {
		public required Int32 ProjectId { get; set; }
		public required String URL { get; set; }
		public DateTime? CommissionDate { get; set; }
		public String? Description { get; set; }
		public String? CommissionReportNumber { get; set; }
	}
}
