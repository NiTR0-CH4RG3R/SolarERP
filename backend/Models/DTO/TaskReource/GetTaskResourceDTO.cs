namespace backend.Models.DTO.TaskReource {
	public class GetTaskResourceDTO : AddTaskResourceDTO {
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
