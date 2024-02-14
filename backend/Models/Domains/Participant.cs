namespace backend.Models.Domains {
	public enum ParticipantCategory {
		Customer, 
		Employee, 
		Guest, 
		Business
	}

	public class Participant {
		public Int32? Id { get; set; }
		public Int32? CompanyId { get; set; }
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
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
