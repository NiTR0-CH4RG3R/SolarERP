using backend.Models.Domains;
using backend.Repositories.Interfaces;
using System.Data;
using Dapper;

namespace backend.Repositories {
	public class RepositoryParticipant : IRepositoryParticipant {

		private readonly IDbConnection _connection;

		public RepositoryParticipant( IDbConnection connection ) {
			_connection = connection;
		}

		public async Task<Participant> CreateAsync( Participant participant ) {
			String sp = "spInsertParticipant";

			var result = await _connection.QueryAsync<Participant>(sp, participant, commandType: CommandType.StoredProcedure);
		
			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No participant was created" );
		}

		public async Task<bool> DeleteAsync( int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Participant>> GetAllByCompanyAndCategoriesAsync( int companyId, ParticipantCategory[] categories ) {
			String sp = "spInsertParticipant";

			var result = await _connection.QueryAsync<Participant>(sp, new {  CompanyId = companyId, Categories = string.Join( ",", categories ) }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<IEnumerable<Participant>> GetAllByCompanyAsync( int companyId ) {
			String sp = "spSelectParticipantsByCompanyId";

			var result = await _connection.QueryAsync<Participant>(sp, new { CompanyId = companyId }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<IEnumerable<Participant>> GetAllEmlployeesByCompanyWithLimitAsync( int companyId, int offset, int count ) {
			String sp = "spSelectParticipantsByCompanyIdEmployeesWithLimit";

			var result = await _connection.QueryAsync<Participant>(sp, new { CompanyId = companyId, offset, count }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<IEnumerable<Participant>> GetAllNonEmlployeesByCompanyWithLimitAsync( int companyId, int offset, int count ) {
			String sp = "spSelectParticipantsByCompanyIdNonEmployeesWithLimit";

			var result = await _connection.QueryAsync<Participant>(sp, new { CompanyId = companyId, offset, count }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<Participant> GetByIdAsync( int id ) {
			String sp = "spSelectParticipantById";

			var result = await _connection.QueryAsync<Participant>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No data found" );
		}

		public async Task<Participant> UpdateAsync( Participant participant ) {
			String sp = "spUpdateParticipantById";

			var result = await _connection.QueryAsync<Participant>(sp, participant, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "Cannot update the participant" );
		}
	}
}
