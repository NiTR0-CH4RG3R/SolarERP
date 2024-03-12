using backend.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace backend.Controllers {
	[Route( "api/[controller]" )]
	[ApiController]
	public class TestController( ILogger<TestController> logger ) : ControllerBase {
		[HttpGet]
		public IActionResult Get() {
			return Ok( "Hello World" );
		}

		[HttpPost( "upload-file" )]
		public async Task<IActionResult> Post( IFormFile formFile ) {
			try {
				String path = await FileManager.SaveFileAsync( formFile, "uploads" );
				return Ok( path );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, "Error saving file" );
				return BadRequest( ex.Message );
			}
		}

		[HttpGet( "download-file" )]
		public async Task<IActionResult> Get( [FromQuery] string path ) {
			try {
				Stream stream = await FileManager.GetFile( path );
				return File( stream, "application/octet-stream" );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, "Error getting file" );
				return BadRequest( ex.Message );
			}
		}
	}
}
