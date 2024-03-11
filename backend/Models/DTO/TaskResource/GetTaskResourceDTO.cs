namespace backend.Models.DTO.TaskResource {
	public class GetTaskResourceDTO : AddTaskResourceDTO {
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
