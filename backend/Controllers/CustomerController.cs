using backend.Models.Domains;
using backend.Models.DTO.Customer;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class CustomerController : ControllerBase {

		private readonly IServiceCustomer _serviceCustomer;
		private readonly ILogger<CustomerController> _logger;

		public CustomerController( IServiceCustomer serviceCustomer, ILogger<CustomerController> logger ) {
			_serviceCustomer = serviceCustomer;
			_logger = logger;
		}

		[HttpGet("all")]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId ) {
			try {
				var customers = await _serviceCustomer.GetAllByCategory( userId, new ParticipantCategory[] { ParticipantCategory.Business, ParticipantCategory.Customer } );
				return Ok( customers );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromQuery] Int32 page, [FromQuery] Int32 pageSize ) {
			try {
				var customers = await _serviceCustomer.GetAllByPagesAsync( userId, page, pageSize );
				return Ok( customers );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet( "{id:int}" )]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromRoute] Int32 id ) {
			try {
				var customer = await _serviceCustomer.GetByIdAsync( id );
				return Ok( customer );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddCustomerDTO customer ) {
			try {
				var result = await _serviceCustomer.CreateAsync( userId, customer );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPut( "{id:int}" )]
		public async Task<IActionResult> Put( [FromQuery] Int32 userId, [FromRoute] Int32 id, [FromBody] AddCustomerDTO customer ) {
			try {
				var result = await _serviceCustomer.UpdateAsync( userId, id, customer );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex.Message );
				return BadRequest( ex.Message );
			}
		}

	}
}
