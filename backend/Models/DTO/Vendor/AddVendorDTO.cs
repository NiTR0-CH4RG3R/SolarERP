namespace backend.Models.DTO.Vendor {
	public class AddVendorDTO {
		public required String Name { get; set; }
		public required String Address { get; set; }
		public String? Email { get; set; }
		public required String Phone01 { get; set; }
		public String? Phone02 { get; set; }
		public String? VendorRegistrationNumber { get; set; }
		public String? Comments { get; set; }
	}
}
