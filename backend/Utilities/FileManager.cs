namespace backend.Utilities {
	public class FileManager {

		private static string rootPath = "wwwroot";

		public static async Task<string> SaveFileAsync( IFormFile file, string directory ) {

			string path = Path.Combine( rootPath, directory );

			if ( !Directory.Exists( path ) ) {
				Directory.CreateDirectory( path );
			}


			if ( file.Length > 0 ) {
				using ( FileStream stream = new FileStream( Path.Combine( path, file.FileName ), FileMode.Create ) ) {
					await file.CopyToAsync( stream );
				}
			}

			return Path.Combine( directory, file.FileName );


		}

		public static void DeleteFile( string path ) {
			if ( File.Exists( path ) ) {
				File.Delete( path );
			}
		}

		public static async Task<Stream> GetFile( string path ) {

			String filepath = Path.Combine( rootPath, path );

			if ( File.Exists( filepath ) ) {
				return new FileStream( filepath, FileMode.Open );
			}

			throw new FileNotFoundException();
		}
	}
}
