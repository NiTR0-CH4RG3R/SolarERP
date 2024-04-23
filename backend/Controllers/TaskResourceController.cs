using backend.Models.DTO.TaskResource;
using backend.Services;
using backend.Services.Interfaces;
using backend.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class TaskResourceController : ControllerBase {

		private readonly IServiceTaskResource _serviceTaskResource;
		private readonly IServiceFileManager _serviceFileManager;
		private readonly ILogger<TaskResourceController> _logger;

		public TaskResourceController( IServiceTaskResource serviceTaskResource,  ILogger<TaskResourceController> logger, IServiceFileManager serviceFileManager ) {
			_serviceTaskResource = serviceTaskResource;
			_logger = logger;
			_serviceFileManager = serviceFileManager;
		}

		[HttpGet]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromQuery] Int32 taskId ) {
			try {
				var result = await _serviceTaskResource.GetAllByTaskIdAsync( userId, taskId );
				return Ok( result );
			}
			catch ( Exception e ) {
				_logger.LogError( e.Message );
				return BadRequest( e.Message );
			}
		}

		[HttpGet( "{taskId:int}" )]
		public async Task<IActionResult> GetByTaskIdAndURL( [FromQuery] Int32 userId, [FromRoute] Int32 taskId, [FromQuery] String URL ) {
			try {
				var taskResource = await _serviceTaskResource.GetByTaskIdAndURLAsync( userId, taskId, URL );
				return Ok( taskResource );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddTaskResourceDTO taskResource ) {
			try {
				// var path = await FileManager.SaveFileAsync( formFile, "task_resources" );
				// taskResource.URL = path;
				var result = await _serviceTaskResource.CreateAsync( userId, taskResource );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex.Message );
				return BadRequest( ex.Message );
			}
		}


		[HttpDelete( "{taskId:int}" )]
		public async Task<IActionResult> Delete( [FromQuery] Int32 userId, [FromRoute] Int32 taskId, [FromQuery] String URL ) {
			try {
				var result = await _serviceTaskResource.DeleteByTaskIdAndURLAsync( userId, taskId, URL );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost( "upload" )]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, IFormFile formFile ) {
			try {
				var result = await _serviceFileManager.SaveFileAsync( userId, formFile, "task/resources" );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

	}
}
