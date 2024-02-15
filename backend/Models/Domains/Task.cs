using System;

namespace backend.Models.Domains {
	public enum TaskCategories {
		Complaint,
		Inquiry,
		Activity,
	}
	public enum TaskUrgencyLevel {
		Critical, High, Neutral, Low, Unknown
	}

	public class Task {
		public Int32? Id { get; set; }
		public required Int32 CompanyId { get; set; }
		public required String Category { get; set; }
		public required Int32 RequestedBy { get; set; }
		public Int32? AssignedTo { get; set; }
		public required String UrgencyLevel { get; set; }
		public Int32? ProjectId { get; set; }
		public String? CallBackNumber { get; set; }
		public String? Description { get; set; }
		public String? Comments { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
