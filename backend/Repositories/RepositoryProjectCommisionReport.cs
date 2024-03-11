using backend.Models.Domains;
using backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace backend.Repositories {
	public class RepositoryProjectCommisionReport( IDbConnection connection ) : IRepositoryProjectCommisionReport {

		private readonly IDbConnection _connection = connection;
		
		public async Task<ProjectCommisionReport> CreateAsync( ProjectCommisionReport projectCommisionReport ) {
			String sp = "spInsertProjectCommisionReport";

			var result = await _connection.QueryAsync<ProjectCommisionReport>( sp, projectCommisionReport, commandType: CommandType.StoredProcedure );

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No project commision report was created" );
		}

		public async Task<bool> DeleteAsync( int id ) {
			String sp = "spDeleteProjectCommisionReport";

			var result = await _connection.QueryAsync<ProjectCommisionReport>( sp, new { Id = id }, commandType: CommandType.StoredProcedure );

			if ( result != null && result.Count() > 0 ) {
				return true;
			}

			throw new Exception( "" );
		}

		public async Task<IEnumerable<ProjectCommisionReport>> GetAllByProjectAsync( int projectId ) {
			String sp = "spSelectProjectCommisionReportsByProjectId";

			var result = await _connection.QueryAsync<ProjectCommisionReport>( sp, new { ProjectId = projectId }, commandType: CommandType.StoredProcedure );

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<IEnumerable<ProjectCommisionReport>> GetAllByProjectWithLimitAsync( int projectId, int offset, int count ) {
			String sp = "spSelectProjectCommisionReportsByProjectIdWithLimit";

			var result = await _connection.QueryAsync<ProjectCommisionReport>( sp, new { ProjectId = projectId, offset, count }, commandType: CommandType.StoredProcedure );

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<ProjectCommisionReport> GetByIdAsync( int id ) {
			String sp = "spSelectProjectCommisionReportById";

			var result = await _connection.QueryAsync<ProjectCommisionReport>( sp, new { Id = id }, commandType: CommandType.StoredProcedure );

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No data found" );
		}

		public async Task<ProjectCommisionReport> UpdateAsync( ProjectCommisionReport projectCommisionReport ) {
			String sp = "spUpdateProjectCommisionReportById";

			var result = await _connection.QueryAsync<ProjectCommisionReport>( sp, projectCommisionReport, commandType: CommandType.StoredProcedure );

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No project commision report was updated" );
		}
	}
}
