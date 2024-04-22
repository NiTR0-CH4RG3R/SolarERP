using backend.Models.DTO.Login;

namespace backend.Services.Interfaces {
	public interface IServiceAuthentication {
		Task<(String, String)?> AuthenticateAsync( LoginDTO login );
		Task<String?> Refresh( Int32 id, String refreshToken );
		Task<Boolean> Logout( Int32 id );
	}
}
