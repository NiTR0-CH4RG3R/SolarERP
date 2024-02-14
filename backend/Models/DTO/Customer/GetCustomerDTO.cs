namespace backend.Models.DTO.Customer {
	public class GetCustomerDTO : AddCustomerDTO {
		public Int32? Id { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
