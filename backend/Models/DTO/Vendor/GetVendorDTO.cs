namespace backend.Models.DTO.Vendor {
	public class GetVendorDTO : AddVendorDTO {
		public Int32? Id { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
