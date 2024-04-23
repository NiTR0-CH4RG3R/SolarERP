using backend.Models.DTO.ProjectResource;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class ProjectResourceController( IServiceProjectResource _serviceProjectResource, IServiceFileManager _serviceFileManager, ILogger<ProjectResourceController> _logger )  : ControllerBase {


		[HttpGet( "all" )]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId ) {
			try {
				throw new NotImplementedException();
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddProjectResourceDTO projectResource ) {
			try {
				var result = await _serviceProjectResource.CreateAsync( userId, projectResource );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromQuery] Int32 projectId, [FromQuery] Int32 page, [FromQuery] Int32 pageSize ) {
			try {
				var result = await _serviceProjectResource.GetAllByProjectAsync( userId, projectId, page, pageSize );
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
				var result = await _serviceProjectResource.GetByIdAsync( id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPut( "{id:int}" )]
		public async Task<IActionResult> Put( [FromQuery] Int32 userId, [FromRoute] Int32 id, [FromBody] AddProjectResourceDTO projectResource ) {
			try {
				var result = await _serviceProjectResource.UpdateAsync( userId, id, projectResource );
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
				await _serviceProjectResource.DeleteAsync( userId, id );
				return Ok();
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost( "upload" )]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, IFormFile formFile ) {
			try {
				var result = await _serviceFileManager.SaveFileAsync( userId, formFile, "projects/resources" );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}
	}
}
