namespace backend.Models.DTO.TaskStatus {
	public class GetTaskStatusDTO : AddTaskStatusDTO {
		public Int32? Id { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
