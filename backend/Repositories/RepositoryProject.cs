using backend.Models.Domains;
using backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace backend.Repositories {
	public class RepositoryProject : IRepositoryProject {
		private readonly IDbConnection _connection;

		public RepositoryProject( IDbConnection connection ) {
			_connection = connection;
		}

		public async Task<Project> CreateAsync( Project project ) {
			String sp = "spInsertProject";

			var result = await _connection.QueryAsync<Project>( sp, project, commandType: CommandType.StoredProcedure );

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No project was created" );
		}

		public async Task<bool> DeleteAsync( int id ) {
			throw new NotImplementedException();
		}

        public async Task<IEnumerable<Project>> GetAllByCompanyAsync(Int32 companyId)
        {
			String sp = "spSelectProjectByCompanyId";

			var result = await _connection.QueryAsync<Project>(sp, new { CompanyId = companyId }, commandType: CommandType.StoredProcedure);

			if(result == null)
			{
				throw new Exception("No data found");
			}
			return result;
        }

        public async Task<IEnumerable<Project>> GetAllByCompanyWithLimitAsync( int companyId, int offset, int count ) {
			String sp = "spSelectProjectsByCompanyIdWithLimit";

			var result = await _connection.QueryAsync<Project>( sp, new { CompanyId = companyId, offset, count }, commandType: CommandType.StoredProcedure );

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<Project> GetByIdAsync( int id ) {
			String sp = "spSelectProjectById";

			var result = await _connection.QueryAsync<Project>( sp, new { Id = id }, commandType: CommandType.StoredProcedure );

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No data found" );
		}

		public async Task<Project> UpdateAsync( Project project ) {
			String sp = "spUpdateProjectById";

			var result = await _connection.QueryAsync<Project>( sp, project, commandType: CommandType.StoredProcedure );

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No project was updated" );
		}
	}
}
