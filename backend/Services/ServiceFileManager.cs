using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using System.IO;

namespace backend.Services {
	public class ServiceFileManager : IServiceFileManager {

		private readonly static String root = "wwwroot";
		private readonly static String uploadDirectory = Path.Combine( root, "uploads" );

		IRepositoryParticipant repositoryParticipant;
		IRepositorySystemUser repositorySystemUser;
		ILogger<ServiceCustomer> logger;

		public ServiceFileManager(
			IRepositoryParticipant repositoryParticipant,
			IRepositorySystemUser repositorySystemUser,
			ILogger<ServiceCustomer> logger
			)
		{
			this.repositoryParticipant = repositoryParticipant;
			this.repositorySystemUser = repositorySystemUser;
			this.logger = logger;

			// Create the upload directory if it doesn't exist
			if ( !Directory.Exists( uploadDirectory ) ) {
				Directory.CreateDirectory( uploadDirectory );
			}
		}

		private static String ConvertPathToPlatformSpecificPath( String path ) {
			return path.Replace( '/', Path.DirectorySeparatorChar );
		}
		
		private static String ConvertPathToUrlPath( String path ) {
			return path.Replace( Path.DirectorySeparatorChar, '/' );
		}

		public async void DeleteFile( Int32 userId, string path ) {
			path = ConvertPathToPlatformSpecificPath( path );

			if ( path.Contains( ".." ) ) {
				throw new UnauthorizedAccessException();
			}

			// Check if the user is an employee
			Int32 companyId = await ServiceUtilities.GetCompanyId( logger, repositoryParticipant, repositorySystemUser, userId );

			String companyDirectory = Path.Combine( uploadDirectory, companyId.ToString() );
			String fullPath = Path.Combine( companyDirectory, path );

			if ( File.Exists( fullPath ) ) {
				File.Delete( fullPath );
			}

			throw new FileNotFoundException();
		}

		public async Task<Stream> GetFile( Int32 userId, string path ) {
			path = ConvertPathToPlatformSpecificPath( path );

			if ( path.Contains( ".." ) ) {
				throw new UnauthorizedAccessException();
			}

			// Check if the user is an employee
			Int32? companyId = await ServiceUtilities.GetCompanyId( logger, repositoryParticipant, repositorySystemUser, userId );

			if ( companyId == null ) {
				throw new UnauthorizedAccessException();
			}
			

			String companyDirectory = Path.Combine( uploadDirectory, companyId.Value.ToString() );
			String fullPath = Path.Combine( companyDirectory, path );

			if ( File.Exists( fullPath ) ) {
				return new FileStream( fullPath, FileMode.Open );
			}

			throw new FileNotFoundException();
		}

		public async Task<string> SaveFileAsync( Int32 userId, IFormFile file, string directory ) {
			// Convert the directory to a platform specific path
			directory = ConvertPathToPlatformSpecificPath( directory );

			// If directory contains a two dots, throw an exception
			if ( directory.Contains( ".." ) ) {
				throw new UnauthorizedAccessException();
			}

			// Check if the user is an employee
			Int32 companyId = await ServiceUtilities.GetCompanyId( logger, repositoryParticipant, repositorySystemUser, userId );

			// Create a ompany directory if it doesn't exist
			String companyDirectory = Path.Combine( uploadDirectory, companyId.ToString() );
			if ( !Directory.Exists( companyDirectory ) ) {
				Directory.CreateDirectory( companyDirectory );
			}

			// Create the requested inside the company directory if it doesn't exist
			String path = companyDirectory;
			
			if ( !String.IsNullOrEmpty( directory ) ) {
				path = Path.Combine( companyDirectory, directory );
			}
			
			// If the directory is a combined path, create the directory
			if ( directory.Contains( Path.DirectorySeparatorChar ) ) {
				String[] directories = directory.Split( Path.DirectorySeparatorChar );
				String currentPath = companyDirectory;
				foreach ( String dir in directories ) {
					currentPath = Path.Combine( currentPath, dir );
					if ( !Directory.Exists( currentPath ) ) {
						Directory.CreateDirectory( currentPath );
					}
				}
			}
			else  if ( !Directory.Exists( path ) ) {
				Directory.CreateDirectory( path );
			}

			// Generate a unique file name
			String fileName = Guid.NewGuid().ToString() + Path.GetExtension( file.FileName );

			// Save the file
			if ( file.Length > 0 ) {
				using ( FileStream stream = new FileStream( Path.Combine( path, fileName ), FileMode.Create ) ) {
					await file.CopyToAsync( stream );
				}
			} else {
				throw new FileLoadException( "File is empty" );
			}

			return ConvertPathToUrlPath(Path.Combine( directory, fileName ));
		}
	}
}
