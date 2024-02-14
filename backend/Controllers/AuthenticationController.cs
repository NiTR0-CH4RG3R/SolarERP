using backend.Models.DTO.Login;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class AuthenticationController : ControllerBase {

		private readonly IServiceAuthentication _serviceAuthentication;

		public AuthenticationController( IServiceAuthentication serviceAuthentication ) {
			_serviceAuthentication = serviceAuthentication;
		}

		[HttpPost( "login" )]
		public async Task<IActionResult> Login( [FromBody] LoginDTO login ) {
			try {

				var result = await _serviceAuthentication.AuthenticateAsync( login );

				if ( result == null ) {
					return Unauthorized();
				}

				HttpContext.Response.Cookies.Append( "refresh_token", result.Value.Item2, new CookieOptions {
					HttpOnly = true,
					SameSite = SameSiteMode.None,
					Secure = true
				} );

				return Ok( result.Value.Item1 );
			}
			catch ( Exception e ) {
				return BadRequest( e.Message );
			}
		}

	}
}
