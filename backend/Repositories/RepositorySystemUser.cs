using backend.Models.Domains;
using backend.Repositories.Interfaces;
using System.Data;
using Dapper;

namespace backend.Repositories {
	public class RepositorySystemUser : IRepositorySystemUser {

		private readonly IDbConnection _connection;

		public RepositorySystemUser( IDbConnection connection ) {
			_connection = connection;
		}

		public async Task<SystemUser> CreateAsync( SystemUser systemUser ) {
			String sp = "spInsertSystemUser";
			var result = await _connection.QueryAsync<SystemUser>(sp, systemUser, commandType: CommandType.StoredProcedure);	
			
			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No system user was created" );
		}

		public async Task<bool> DeleteAsync( int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<SystemUser>> GetAllByCompanyAsync( int companyId ) {
			String sp = "spSelectSystemUsersByCompanyId";
			var result = await _connection.QueryAsync<SystemUser>( sp, new { CompanyId = companyId }, commandType: CommandType.StoredProcedure );

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<IEnumerable<SystemUser>> GetAllByCompanyWithLimitAsync( int companyId, int offset, int count ) {
			String sp = "spSelectSystemUsersByCompanyIdWithLimit";
			var result = await _connection.QueryAsync<SystemUser>( sp, new { CompanyId = companyId, offset, count }, commandType: CommandType.StoredProcedure );

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<SystemUser> GetByIdAsync( int id ) {
			String sp = "spSelectSystemUserById";
			var result = await _connection.QueryAsync<SystemUser>( sp, new { Id = id }, commandType: CommandType.StoredProcedure );

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result.First();
		}

		public async Task<SystemUser> GetByUsernameAndPasswordAsync( string username, string password ) {
			String sp = "spSelectSystemUserByUsernameAndPassword";
			var result = await _connection.QueryFirstOrDefaultAsync<SystemUser>( sp, new { Username = username, Password = password }, commandType: CommandType.StoredProcedure );

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<SystemUser> UpdateAsync( SystemUser systemUser ) {
			String sp = "spUpdateSystemUserById";
			var result = await _connection.QueryAsync<SystemUser>( sp, systemUser, commandType: CommandType.StoredProcedure );
			if ( result == null ) {
				throw new Exception( "No system user was updated" );
			}

			return result.First();
		}

		public async Task<SystemUserLogin> InsertSystemUserLoginAsync( SystemUserLogin systemUserLogin ) {
			String sp = "spInsertSystemUserLogin";
			var result = await _connection.QueryAsync<SystemUserLogin>( sp, new { 
				systemUserId = systemUserLogin.SystemUserId, 
				accessToken =  systemUserLogin.AccessToken,
				refreshToken = systemUserLogin.RefreshToken
			}, commandType: CommandType.StoredProcedure );
			if ( result == null ) {
				throw new Exception( "No system user login was created" );
			}

			return result.First();
		}

		public async Task<SystemUserLogin> GetSystemUserLoginByIdAsync( Int32 id ) {
			String sp = "spSelectSystemUserLoginById";
			var result = await _connection.QueryAsync<SystemUserLogin>( sp, new { systemUserId = id }, commandType: CommandType.StoredProcedure );
			if ( result == null ) {
				throw new Exception( "No system user login was found" );
			}
			return result.First();
		}

		public async Task<SystemUserLogin> UpdateSystemUserLoginAccessTokenAsync( Int32 id, String accessToken ) {
			String sp = "spUpdateSystemUserLoginAccessToken";

			var result = await _connection.QueryAsync<SystemUserLogin>( sp, new { systemUserId = id, accessToken }, commandType: CommandType.StoredProcedure );

			if ( result == null ) {
				throw new Exception( "No system user login was updated" );
			}

			return result.First();
		}

		public async void DeleteSystemUserLoginByIdAsync( Int32 id ) {
			String sp = "spDleeteSystemUserLogin";
			await _connection.ExecuteAsync( sp, param: new { systemUserId = id }, commandType: CommandType.StoredProcedure );
		}
	}
}
