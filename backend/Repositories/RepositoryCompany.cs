using backend.Models.Domains;
using backend.Repositories.Interfaces;
using System.Data;
using Dapper;

namespace backend.Repositories {
	public class RepositoryCompany : IRepositoryCompany {

		private readonly IDbConnection _connection;

		public RepositoryCompany( IDbConnection connection ) {
			_connection = connection;
		}

		public async Task<Company> CreateAsync( Company company ) {
			String sp = "spInsertCompany";
			var result = await _connection.QueryAsync<Company>(sp, company, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No company was created" );
		}

		public async Task<bool> DeleteAsync( int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Company>> GetAllAsync() {
			String sp = "spSelectCompanies";
			var result = await _connection.QueryAsync<Company>(sp, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<Company> GetByIdAsync( int id ) {
			String sp = "spSelectCompanyById";
			var result = await _connection.QueryAsync<Company>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No data found" );
		}

		public async Task<Company> UpdateAsync( Company company ) {
			String sp = "spUpdateCompanyById";
			var result = await _connection.QueryAsync<Company>(sp, company, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "Cannot update the company" );
		}
	}
}
