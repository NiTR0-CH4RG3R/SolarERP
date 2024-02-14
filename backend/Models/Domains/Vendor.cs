namespace backend.Models.Domains {
	public class Vendor {
		public Int32? Id { get; set; }
		public Int32? CompanyId { get; set; }
		public required String Name { get; set; }
		public required String Address { get; set; }
		public String? Email { get; set; }
		public required String Phone01 { get; set; }
		public String? Phone02 { get; set; }
		public String? VendorRegistrationNumber { get; set; }
		public String? Comments { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
