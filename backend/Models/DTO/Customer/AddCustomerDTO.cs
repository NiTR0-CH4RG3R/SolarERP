namespace backend.Models.DTO.Customer {
	public class AddCustomerDTO {
		public required String FirstName { get; set; }
		public String? LastName { get; set; }
		public required String Category { get; set; }
		public required String Address { get; set; }
		public String? Email { get; set; }
		public required String Phone01 { get; set; }
		public String? Phone02 { get; set; }
		public String? CustomerRegistrationNumber { get; set; }
		public String? Profession { get; set; }
		public String? Comments { get; set; }
	}
}
