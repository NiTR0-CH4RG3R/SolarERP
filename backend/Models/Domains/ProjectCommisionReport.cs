namespace backend.Models.Domains {
	public class ProjectCommisionReport {
		public required Int32 ProjectId { get; set; }
		public required String URL { get; set; }
		public DateTime? CommissionDate { get; set; }
		public String? Description { get; set; }
		public String? CommissionReportNumber { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
