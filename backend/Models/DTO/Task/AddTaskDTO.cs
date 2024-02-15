namespace backend.Models.DTO.Task {
	public class AddTaskDTO {
		public required String Category { get; set; }
		public required Int32 RequestedBy { get; set; }
		public Int32? AssignedTo { get; set; }
		public required String UrgencyLevel { get; set; }
		public Int32? ProjectId { get; set; }
		public String? CallBackNumber { get; set; }
		public String? Description { get; set; }
		public String? Comments { get; set; }
	}
}
