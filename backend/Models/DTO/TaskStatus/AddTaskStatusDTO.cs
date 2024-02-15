namespace backend.Models.DTO.TaskStatus {
	public class AddTaskStatusDTO {
		public required Int32 TaskId { get; set; }
		public required String Status { get; set; }
		public String? Comments { get; set; }
	}
}
