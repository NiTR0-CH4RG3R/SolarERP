using backend.Models.DTO.VendorItem;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class VendorItemController : ControllerBase {
		private readonly IServiceVendorItem _serviceVendorItem;
		private readonly ILogger<VendorItemController> _logger;

		public VendorItemController( IServiceVendorItem serviceVendorItem, ILogger<VendorItemController> logger ) {
			_serviceVendorItem = serviceVendorItem;
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
				var result = await _serviceVendorItem.GetAllAsync( userId, page, pageSize );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet("byVendor")]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromQuery] Int32 vendorId, [FromQuery] Int32 page, [FromQuery] Int32 pageSize ) {
			try {
				var result = await _serviceVendorItem.GetAllByVendorAsync( userId, vendorId, page, pageSize );
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
				var result = await _serviceVendorItem.GetByIdAsync( id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddVendorItemDTO vendorItem ) {
			try {
				var result = await _serviceVendorItem.CreateAsync( userId, vendorItem );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPut( "{id:int}" )]
		public async Task<IActionResult> Put( [FromQuery] Int32 userId, [FromRoute] Int32 id, [FromBody] AddVendorItemDTO vendorItem ) {
			try {
				var result = await _serviceVendorItem.UpdateAsync( userId, id, vendorItem );
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
				var result = await _serviceVendorItem.DeleteAsync( userId, id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}
	}
}
