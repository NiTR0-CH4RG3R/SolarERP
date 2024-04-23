namespace backend.Services.Interfaces {
	public interface IServiceFileManager {
		Task<string> SaveFileAsync( Int32 userId, IFormFile file, string directory );
		void DeleteFile( Int32 userId, string path );
		Task<Stream> GetFile( Int32 userId, string path );
	}
}
