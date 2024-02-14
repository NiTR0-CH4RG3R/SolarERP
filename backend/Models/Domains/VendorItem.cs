namespace backend.Models.Domains {
	public class VendorItem {
		public Int32? Id { get; set; }
		public required Int32 VendorId { get; set; }
		public required String ProductName { get; set; }
		public String? ProductCode { get; set; }
		public String? Brand { get; set; }
		public required float Price { get; set; }
		public String? WarrantyDuration { get; set; }
		public Int32? Capacity { get; set; }
		public String? Comments { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
