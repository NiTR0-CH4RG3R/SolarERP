using backend.Models.Domains;
using backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace backend.Repositories {
	public class RepositoryVendorItem : IRepositoryVendorItem {

		private readonly IDbConnection _connection;

		public RepositoryVendorItem( IDbConnection connection ) {
			_connection = connection;
		}


		public async Task<VendorItem> CreateAsync( VendorItem vendorItem ) {
			String sp = "spInsertVendorItem";

			var result = await _connection.QueryAsync<VendorItem>(sp, vendorItem, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No vendor item was created" );
		}

		public async Task<bool> DeleteAsync( int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<VendorItem>> GetAllByCompanyWithLimitAsync( int companyId, int offset, int count ) {
			String sp = "spSelectVendorItemsByCompanyIdWithLimit";

			var result = await _connection.QueryAsync<VendorItem>(sp, new { CompanyId = companyId, offset, count }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<IEnumerable<VendorItem>> GetAllByVendorWithLimitAsync( int vendorId, int offset, int count ) {
			String sp = "spSelectVendorItemsByVendorIdWithLimit";

			var result = await _connection.QueryAsync<VendorItem>(sp, new { VendorId = vendorId, offset, count }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<VendorItem> GetByIdAsync( int id ) {
			String sp = "spSelectVendorItemById";

			var result = await _connection.QueryAsync<VendorItem>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No data found" );
		}

		public async Task<VendorItem> UpdateAsync( VendorItem vendorItem ) {
			String sp = "spUpdateVendorItemById";

			var result = await _connection.QueryAsync<VendorItem>(sp, vendorItem, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No vendor item was updated" );
		}
	}
}
