using backend.Models.DTO.Project;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class ProjectController : ControllerBase {
		private readonly IServiceProject _serviceProject;

		private readonly ILogger<ProjectController> _logger;

		public ProjectController( IServiceProject serviceProject, ILogger<ProjectController> logger ) {
			_serviceProject = serviceProject;
			_logger = logger;
		}

		[HttpGet( "all" )]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId ) {
			try {
				var result = await _serviceProject.GetAllAsync(userId);
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromQuery] Int32 page, [FromQuery] Int32 pageSize ) {
			try {
				var result = await _serviceProject.GetAllWithLimitAsync( userId, page, pageSize );
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
				var result = await _serviceProject.GetByIdAsync( id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet("Customer/{customerId:int}")]

		public async Task<IActionResult> GetProjectsByCustomerId([FromQuery] Int32 userId, [FromRoute] Int32 customerId)
		{
			try {
				var result = await _serviceProject.GetAllByCustomerId(userId, customerId);
				return Ok( result );
			}
			catch ( Exception ex )
			{
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
		}


		[HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddProjectDTO project ) {
			try {
				var result = await _serviceProject.CreateAsync( userId, project );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPut( "{id:int}" )]
		public async Task<IActionResult> Put( [FromQuery] Int32 userId, [FromRoute] Int32 id, [FromBody] AddProjectDTO project ) {
			try {
				var result = await _serviceProject.UpdateAsync( userId, id, project );
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
				var result = await _serviceProject.DeleteAsync( userId, id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}


	}
}
