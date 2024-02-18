namespace backend.Models.Domains {
	public class ProjectItem {
		public Int32? Id { get; set; }
		public required Int32 ProjectId { get; set; }
		public required Int32 VendorItemId { get; set; }
		public String? ModuleNo { get; set; }
		public String? WarrantyDuration { get; set; }
		public String? SerialNo { get; set; }
		public String? Comments { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
