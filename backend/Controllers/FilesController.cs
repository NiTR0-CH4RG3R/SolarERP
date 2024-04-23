using backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class FilesController( IServiceFileManager _serviceFileManager, ILogger<ProjectResourceController> _logger ) : ControllerBase {

		[HttpPost]
		public async Task<IActionResult> Post( [FromQuery] Int32 userId, [FromQuery] String directory , IFormFile formFile ) {
			try {
				var result = await _serviceFileManager.SaveFileAsync( userId, formFile, directory );
				return Ok( result );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet]
		public async Task<IActionResult> Get( [FromQuery] Int32 userId, [FromQuery] string path ) {
			try {
				var stream = await _serviceFileManager.GetFile( userId, path );
				return File( stream, "application/octet-stream", fileDownloadName: path.Split( '/' ).Last() );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
				return BadRequest( ex.Message );
			}
		}
	}
}
