using backend.Models.DTO.Login;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[Produces( "application/json" )]
	[Authorize( Roles = "User, Admin" )]
	[ApiController]
	public class AuthenticationController : ControllerBase {

		private readonly IServiceAuthentication _serviceAuthentication;

		public AuthenticationController( IServiceAuthentication serviceAuthentication ) {
			_serviceAuthentication = serviceAuthentication;
		}

		[AllowAnonymous]
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

		[HttpPost( "logout" )]
		public async Task<IActionResult> Logout( [FromQuery] Int32 userId ) {
			try {
				await _serviceAuthentication.Logout( userId );
				HttpContext.Response.Cookies.Delete( "refresh_token" );
				return Ok();
			}
			catch ( Exception e ) {
				return BadRequest( e.Message );
			}
		}

		[HttpPost( "refresh" )]
		public async Task<IActionResult> Refresh( [FromQuery] Int32 userId ) {
			try {
				var refreshToken = HttpContext.Request.Cookies["refresh_token"];
				if ( refreshToken == null ) {
						return Unauthorized();
				}

				var result = await _serviceAuthentication.Refresh( userId, refreshToken );

				if ( result == null ) {
					return Unauthorized();
				}

				return Ok( result );
			}
			catch ( Exception e ) {
				return BadRequest( e.Message );
			}
		}

	}
}
