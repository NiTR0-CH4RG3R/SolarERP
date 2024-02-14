using backend.Models.Domains;
using backend.Models.DTO.Login;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Services {
	public class ServiceAuthentication : IServiceAuthentication {
		private readonly IRepositorySystemUser _repositorySystemUser;
		private readonly IConfiguration _configuration;

		public ServiceAuthentication( IRepositorySystemUser repositorySystemUser, IConfiguration configuration ) {
			_repositorySystemUser = repositorySystemUser;
			_configuration = configuration;
		}

		public async Task<(String, String)?> AuthenticateAsync( LoginDTO login ) {
			// Get the user by username and password
			var user = await _repositorySystemUser.GetByUsernameAndPasswordAsync( login.Username, login.Password );

			if ( user == null ) {
				return null;
			}

			// Generate the token
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

			List<Claim> claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
			claims.Add(new Claim(ClaimTypes.Role, SystemUserRoles.User.ToString()));
			if ( user.Role == SystemUserRoles.Admin.ToString() )
				claims.Add( new Claim( ClaimTypes.Role, SystemUserRoles.Admin.ToString() ) );

			var tokenDescriptor = new SecurityTokenDescriptor {
				Subject = new ClaimsIdentity(claims.ToArray()),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			return (tokenString, "");

		}
	}
}
