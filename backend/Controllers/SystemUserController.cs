using backend.Models.DTO.SystemUser;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class SystemUserController : ControllerBase {

		private readonly IServiceSystemUser _serviceSystemUser;
		private readonly ILogger<SystemUserController> _logger;

		public SystemUserController( IServiceSystemUser serviceSystemUser, ILogger<SystemUserController> logger ) {
			_serviceSystemUser = serviceSystemUser;
			_logger = logger;
		}

		[HttpGet("all")]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId ) {
			try {
				var result = await _serviceSystemUser.GetAllAsync( userId );
				return Ok( result );
			}
			catch ( Exception e ) {
				_logger.LogError( e.Message );
				return BadRequest( e.Message );
			}
		}

		[HttpGet]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromQuery] Int32 page, [FromQuery] Int32 pageSize ) {
			try {
				var result = await _serviceSystemUser.GetAllWithLimitAsync( userId, page, pageSize );
				return Ok( result );
			}
			catch ( Exception e ) {
				_logger.LogError( e.Message );
				return BadRequest( e.Message );
			}
		}

		[HttpGet( "{id:int}" )]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromRoute] Int32 id ) {
			try {
				var systemUser = await _serviceSystemUser.GetByIdAsync( id );
				return Ok( systemUser );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddSystemUserDTO systemUser ) {
			try {
				var result = await _serviceSystemUser.CreateAsync( userId, systemUser );
				return Ok( result );
			}
			catch ( Exception e ) {
				_logger.LogError( e.Message );
				return BadRequest( e.Message );
			}
		}

		[HttpPut( "{id:int}" )]
		public async Task<IActionResult> Put( [FromQuery] Int32 userId, [FromRoute] Int32 id, [FromBody] AddSystemUserDTO systemUser ) {
			try {
				var result = await _serviceSystemUser.UpdateAsync( userId, id, systemUser );
				return Ok( result );
			}
			catch ( Exception e ) {
				_logger.LogError( e.Message );
				return BadRequest( e.Message );
			}
		}
	}
}
