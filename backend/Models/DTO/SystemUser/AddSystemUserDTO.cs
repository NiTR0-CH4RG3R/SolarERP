using backend.Models.Domains;

namespace backend.Models.DTO.SystemUser {
	public class AddSystemUserDTO {
		public required String FirstName { get; set; }
		public String? LastName { get; set; }
		public required String Address { get; set; }
		public String? Email { get; set; }
		public required String Phone01 { get; set; }
		public String? Phone02 { get; set; }
		public String? CustomerRegistrationNumber { get; set; }
		public String? Profession { get; set; }
		public String? Comments { get; set; }
		public required String Role { get; set; } = SystemUserRoles.User.ToString();
		public required String Username { get; set; }
		public required String Password { get; set; }
		public String? ProfilePicture { get; set; }
	}
}
