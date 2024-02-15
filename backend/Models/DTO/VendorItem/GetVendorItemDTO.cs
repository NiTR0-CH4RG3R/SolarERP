namespace backend.Models.DTO.VendorItem {
	public class GetVendorItemDTO : AddVendorItemDTO {
		public Int32? Id { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
