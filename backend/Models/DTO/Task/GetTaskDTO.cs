namespace backend.Models.DTO.Task {
	public class GetTaskDTO : AddTaskDTO {
		public Int32? Id { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
