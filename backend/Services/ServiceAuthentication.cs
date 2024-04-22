using backend.Models.Domains;
using backend.Models.DTO.Login;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Services {
	public class ServiceAuthentication( IRepositorySystemUser _repositorySystemUser, IConfiguration _configuration, ILogger<ServiceAuthentication> _logger ) : IServiceAuthentication {

		private static String GenerateToken( SystemUser user, String secret, DateTime expiresIn ) {
			var tokenHandler = new JwtSecurityTokenHandler();

			List<Claim> claims = new List<Claim>();

			// Add the user id and roles to the claims
			claims.Add( new Claim( type: "userId", value: user.Id.Value.ToString() ) );

			claims.Add( new Claim( ClaimTypes.Role, SystemUserRoles.User.ToString() ) );
			if ( user.Role == SystemUserRoles.Admin.ToString() )
				claims.Add( new Claim( ClaimTypes.Role, SystemUserRoles.Admin.ToString() ) );

			var tokenSecretStream = Encoding.ASCII.GetBytes(secret);

			var tokenDescriptor = new SecurityTokenDescriptor {
				Subject = new ClaimsIdentity(claims.ToArray()),
				Expires = expiresIn,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenSecretStream), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		private static bool ValidateToken( String token, String secret ) {
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenSecretStream = Encoding.ASCII.GetBytes(secret);

			try {
				tokenHandler.ValidateToken(token, new TokenValidationParameters {
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					IssuerSigningKey = new SymmetricSecurityKey(tokenSecretStream)
				}, out SecurityToken validatedToken);
			}
			catch {
				return false;
			}

			return true;
		}

		public async Task<(String, String)?> AuthenticateAsync( LoginDTO login ) {
			// Get the user by username and password
			var user = await _repositorySystemUser.GetByUsernameAndPasswordAsync( login.Username, login.Password );

			if ( user == null || user.Id == null ) {
				return null;
			}

			var accessTokenSecret =_configuration["Jwt:AccessTokenSecret"];
			var refreshTokenSecret =_configuration["Jwt:RefreshTokenSecret"];

			if ( accessTokenSecret == null || refreshTokenSecret == null )
				throw new Exception( "Jwt:AccessTokenSecret or Jwt:RefreshTokenSecret is not set in appsettings.json" );


			// If the user is already logged in, delete the existing login
			try {
				var userlogin = await _repositorySystemUser.GetSystemUserLoginByIdAsync( user.Id.Value );
				if ( userlogin != null ) {
					_repositorySystemUser.DeleteSystemUserLoginByIdAsync( user.Id.Value );
				}
			}
			catch (Exception e) {
				_logger.LogWarning( e, "Error while trying to delete system user login by system user id" );
			}

			var accessToken = GenerateToken( user, accessTokenSecret, DateTime.UtcNow.AddMinutes(1.0) );
			var refreshToken = GenerateToken( user, refreshTokenSecret, DateTime.UtcNow.AddHours(1.0) );

			// Store the refresh token in the database

			try {

				await _repositorySystemUser.InsertSystemUserLoginAsync(
						new SystemUserLogin {
							SystemUserId = user.Id.Value,
							AccessToken = accessToken,
							RefreshToken = refreshToken
						}
					);
			}
			catch (Exception e) {
				_logger.LogError( e, "Error inserting system user login to the database" );
			}

			return (accessToken, refreshToken);

		}

		public async Task<String?> Refresh( Int32 id, String refreshToken ) {
			// Check whether the refresh token exists in the database
			
			SystemUserLogin? systemUserLogin = null;
			SystemUser? systemUser = null;

			try { 
				systemUserLogin = await _repositorySystemUser.GetSystemUserLoginByIdAsync( id );
				systemUser = await _repositorySystemUser.GetByIdAsync( id );
			}
			catch (Exception e) {
				_logger.LogError( e, "Error while trying to get system user login by id" );
				await Logout( id );
				return null;
			}

			var accessTokenSecret =_configuration["Jwt:AccessTokenSecret"];
			var refreshTokenSecret =_configuration["Jwt:RefreshTokenSecret"];

			if ( accessTokenSecret == null || refreshTokenSecret == null )
				throw new Exception( "Jwt:AccessTokenSecret or Jwt:RefreshTokenSecret is not set in appsettings.json" );

			// Validate the refresh token
			if ( !ValidateToken( refreshToken, refreshTokenSecret ) ) {
				await Logout(id);
				return null;
			}

			// Refresh token is valid, generate a new access token
			var accessToken = GenerateToken( systemUser, accessTokenSecret, DateTime.UtcNow.AddMinutes(1.0) );

			// Store the refresh token in the database
			try { 
				await _repositorySystemUser.UpdateSystemUserLoginAccessTokenAsync(id, accessToken);
			}
			catch (Exception e) {
				_logger.LogError( e, "Error inserting system user login to the database" );
			}
			
			return accessToken;
		}
	
		public async Task<Boolean> Logout( Int32 id ) {
			try {
				_repositorySystemUser.DeleteSystemUserLoginByIdAsync( id );
				return true;
			}
			catch (Exception e) {
				_logger.LogError( e, "Error while trying to delete system user login by id" );
				return false;
			}
		}
	}
}
