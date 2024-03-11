using backend.Models.DTO.ProjectTest;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class ProjectTestController( IServiceProjectTest serviceProjectTest, ILogger<ProjectTestController> logger ) : ControllerBase {

		[HttpGet( "all" )]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId ) {
			try {
				throw new NotImplementedException();
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddProjectTestDTO projectTest ) {
			try {
				var result = await serviceProjectTest.CreateAsync( userId, projectTest );
				return Ok( result );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromQuery] Int32 projectId, [FromQuery] Int32 page, [FromQuery] Int32 pageSize ) {
			try {
				var result = await serviceProjectTest.GetAllByProjectAsync( userId, projectId, page, pageSize );
				return Ok( result );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet( "{id:int}" )]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromRoute] Int32 id ) {
			try {
				var result = await serviceProjectTest.GetByIdAsync( id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPut( "{id:int}" )]
		public async Task<IActionResult> Put( [FromQuery] Int32 userId, [FromRoute] Int32 id, [FromBody] AddProjectTestDTO projectTest ) {
			try {
				var result = await serviceProjectTest.UpdateAsync( userId, id, projectTest );
				return Ok( result );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}
	}
}
