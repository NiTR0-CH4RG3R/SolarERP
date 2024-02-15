﻿namespace backend.Models.DTO.TaskReource {
	public class AddTaskResourceDTO {
		public required Int32 TaskId { get; set; }
		public required String URL { get; set; }
		public required String Category { get; set; }
		public String? Comments { get; set; }
	}
}
