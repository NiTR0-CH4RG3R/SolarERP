namespace backend.Models.Domains {
	public class SystemUserLogin {
		public Int32 SystemUserId { get; set; }
		public required String AccessToken { get; set; }
		public required String RefreshToken { get; set; }
		public DateTime? LoggedInTime { get; set; }

	}
}
