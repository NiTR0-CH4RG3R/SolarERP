using backend.Models.Domains;
using backend.Repositories.Interfaces;
using System.Data;
using Dapper;

namespace backend.Repositories {
	public class RepositoryVendor : IRepositoryVendor {

		private readonly IDbConnection _connection;

		public RepositoryVendor( IDbConnection connection ) {
			_connection = connection;
		}

		public async Task<Vendor> CreateAsync( Vendor vendor ) {
			String sp = "spInsertVendor";

			var result = await _connection.QueryAsync<Vendor>(sp, vendor, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No participant was created" );
		}

		public async Task<bool> DeleteAsync( int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Vendor>> GetAllByCompanyWithLimitAsync( int companyId, int offset, int count ) {
			String sp = "spSelectVendorsByCompanyIdWithLimit";

			var result = await _connection.QueryAsync<Vendor>(sp, new { CompanyId = companyId, offset, count }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<Vendor> GetByIdAsync( int id ) {
			String sp = "spSelectVendorById";

			var result = await _connection.QueryAsync<Vendor>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No data found" );
		}

		public async Task<Vendor> UpdateAsync( Vendor vendor ) {
			String sp = "spUpdateVendorById";

			var result = await _connection.QueryAsync<Vendor>(sp, vendor, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No vendor was updated" );

		}
	}
}
