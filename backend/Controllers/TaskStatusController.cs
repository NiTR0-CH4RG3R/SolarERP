using backend.Models.DTO.TaskStatus;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class TaskStatusController : ControllerBase {
		private readonly IServiceTaskStatus _serviceTaskStatus;
		private readonly ILogger<TaskStatusController> _logger;

		public TaskStatusController( IServiceTaskStatus serviceTaskStatus, ILogger<TaskStatusController> logger ) {
			_serviceTaskStatus = serviceTaskStatus;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromQuery] Int32 taskId, [FromQuery] Int32 page, [FromQuery] Int32 pageSize) {
			try {
				var result = await _serviceTaskStatus.GetAllByTaskAsync( userId, taskId, page, pageSize );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet( "{id:int}" )]
		public async Task<IActionResult> GetById( [FromQuery] Int32 userId, [FromRoute] Int32 id ) {
			try {
				var result = await _serviceTaskStatus.GetByIdAsync( id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddTaskStatusDTO taskStatus ) {
			try {
				var result = await _serviceTaskStatus.CreateAsync( userId, taskStatus );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPut( "{id:int}" )]
		public async Task<IActionResult> Put( [FromQuery] Int32 userId, [FromRoute] Int32 id, [FromBody] AddTaskStatusDTO taskStatus ) {
			try {
				var result = await _serviceTaskStatus.UpdateAsync( userId, id, taskStatus );
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
				await _serviceTaskStatus.DeleteAsync( userId, id );
				return Ok();
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

	}
}
