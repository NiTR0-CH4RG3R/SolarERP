namespace backend.Models.DTO.ProjectItem {
	public class AddProjectItemDTO {
		public required Int32 ProjectId { get; set; }
		public required Int32 VendorItemId { get; set; }
		public String? ModuleNo { get; set; }
		public String? WarrantyDuration { get; set; }
		public String? SerialNo { get; set; }
		public String? Comments { get; set; }
	}
}
