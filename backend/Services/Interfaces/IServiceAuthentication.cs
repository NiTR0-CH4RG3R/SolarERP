using backend.Models.DTO.Login;

namespace backend.Services.Interfaces {
	public interface IServiceAuthentication {
		Task<(String, String)?> AuthenticateAsync( LoginDTO login );
	}
}
