namespace backend.Models.Domains {
	public enum SystemUserRoles {
		User,
		Admin, 
	}

	public class SystemUser {
		public Int32? Id { get; set; }
		public required String Role { get; set; } = SystemUserRoles.User.ToString();
		public required String Username { get; set; }
		public required String Password { get; set; }
		public String? ProfilePicture { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
