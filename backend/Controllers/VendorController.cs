using backend.Models.DTO.Vendor;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class VendorController : ControllerBase {
		private readonly IServiceVendor _serviceVendor;
		private readonly ILogger<VendorController> _logger;

		public VendorController( IServiceVendor serviceVendor, ILogger<VendorController> logger ) {
			_serviceVendor = serviceVendor;
			_logger = logger;
		}

		[HttpGet( "all" )]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId ) {
			try {
				throw new NotImplementedException();
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
			}
		}

		[HttpGet]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromQuery] Int32 page, [FromQuery] Int32 pageSize ) {
			try {
				var result = await _serviceVendor.GetAllAsync( userId, page, pageSize );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet( "{id:int}" )]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromRoute] Int32 id ) {
			try {
				var result = await _serviceVendor.GetByIdAsync( id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddVendorDTO vendor ) {
			try {
				var result = await _serviceVendor.CreateAsync( userId, vendor );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPut( "{id:int}" )]
		public async Task<IActionResult> Put( [FromQuery] Int32 userId, [FromRoute] Int32 id, [FromBody] AddVendorDTO vendor ) {
			try {
				var result = await _serviceVendor.UpdateAsync( userId, id, vendor );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpDelete( "{id:int}" )]
		public async Task<IActionResult> Delete( [FromQuery] Int32 userId, [FromRoute] Int32 id ) {
			try {
				var result = await _serviceVendor.DeleteAsync( userId, id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

	}
}
