using backend.Models.Domains;
using backend.Models.DTO.Task;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class TaskController : ControllerBase {
		private readonly IServiceTask _serviceTask;
		private readonly ILogger<TaskController> _logger;

		public TaskController( IServiceTask serviceTask, ILogger<TaskController> logger ) {
			_serviceTask = serviceTask;
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
				var result = await _serviceTask.GetAllAsync( userId, page, pageSize );
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
				var result = await _serviceTask.GetByIdAsync( id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}
		
		[HttpGet("category")]
		public async Task<IActionResult> GetTaskDetailsByCategory( [FromQuery] Int32 userId, [FromQuery] TaskCategories category, [FromQuery] Int32 page, [FromQuery] Int32 pageSize)
		{
			try
			{
				var result = await _serviceTask.GetAllByCategory(userId, category, page, pageSize);
				return Ok( result );
			}
			catch ( Exception ex )
			{
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
		}
		
        [HttpGet("urgencylevel")]
        public async Task<IActionResult> GetTaskDetailsByUrgencyLevel([FromQuery] Int32 userId, [FromQuery] TaskUrgencyLevel urgencyLevel, [FromQuery] Int32 page, [FromQuery] Int32 pageSize)
        {
            try
            {
                var result = await _serviceTask.GetAllByUrgencyLevel(userId, urgencyLevel, page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

		[HttpGet("category+urgencylevel")]

		public async Task<IActionResult> GetTaskDetailsByCategoryAndUrgencyLevel([FromQuery] Int32 userId, [FromQuery] TaskCategories category, [FromQuery] TaskUrgencyLevel urgencyLevel, [FromQuery] Int32 page, [FromQuery] Int32 pageSize)
		{
            try
            {
                var result = await _serviceTask.GetAllByCategoryAndUrgencyLevel(userId, category, urgencyLevel, page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromBody] AddTaskDTO task ) {
			try {
				var result = await _serviceTask.CreateAsync( userId, task );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpPut( "{id:int}" )]
		public async Task<IActionResult> Put( [FromQuery] Int32 userId, [FromRoute] Int32 id, [FromBody] AddTaskDTO task ) {
			try {
				var result = await _serviceTask.UpdateAsync( userId, id, task );
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
				var result = await _serviceTask.DeleteAsync( userId, id );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}
	}
}
