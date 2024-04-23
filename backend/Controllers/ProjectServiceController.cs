using backend.Models.DTO.ProjectService;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class ProjectServiceController : ControllerBase {
		
		private readonly IServiceProjectService _serviceProjectService;
		private readonly IServiceFileManager _serviceFileManager;
		private readonly ILogger<ProjectServiceController> _logger;

		public ProjectServiceController( IServiceProjectService serviceProjectService,  ILogger<ProjectServiceController> logger, IServiceFileManager serviceFileManager ) {
			_serviceProjectService = serviceProjectService;
			_logger = logger;
			_serviceFileManager = serviceFileManager;
		}

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
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddProjectServiceDTO projectService ) {
			try {
				var result = await _serviceProjectService.CreateAsync( userId, projectService );
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
				var result = await _serviceProjectService.GetAllByProjectIdAsync( userId, projectId, page, pageSize );
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
				var result = await _serviceProjectService.GetByIdAsync( id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPut( "{id:int}" )]
		public async Task<IActionResult> Put( [FromQuery] Int32 userId, [FromRoute] Int32 id, [FromBody] AddProjectServiceDTO projectService ) {
			try {
				var result = await _serviceProjectService.UpdateAsync( userId, id, projectService );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost( "upload" )]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, IFormFile formFile ) {
			try {
				var result = await _serviceFileManager.SaveFileAsync( userId, formFile, "projects/service_reports" );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}
	}
}
