﻿using backend.Models.Domains;
using backend.Repositories.Interfaces;
using Dapper;
using MySqlX.XDevAPI.Common;
using System.Data;

namespace backend.Repositories {
	public class RepositoryTask : IRepositoryTask {

		private readonly IDbConnection _connection;

		public RepositoryTask( IDbConnection connection ) {
			_connection = connection;
		}

		public async Task<Models.Domains.Task> CreateAsync( Models.Domains.Task task ) {
			String sp = "spInsertTask";

			var result = await _connection.QueryAsync<Models.Domains.Task>(sp, task, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No task was created" );
		}

		public async Task<bool> DeleteAsync( int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Models.Domains.Task>> GetAllByAssignedToWithLimitAsync( int userId, int offset, int count ) {
			String sp = "spSelectTasksBySystemUserIdWithLimit";

			var result = await _connection.QueryAsync<Models.Domains.Task>(sp, new { SystemUserId = userId, offset, count }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

        public async Task<IEnumerable<Models.Domains.Task>> GetAllByCompanyAndCategoryWithLimitAsync(int companyId, TaskCategories category, int offset, int count)
        {
            String sp = "spSelectTasksByCompanyAndCategoryWithLimit";

            var result = await _connection.QueryAsync<Models.Domains.Task>(sp, new { CompanyId = companyId, Category = category.ToString(), offset, count }, commandType: CommandType.StoredProcedure);

            if (result == null)
            {
                throw new Exception("No data found");
            }

            return result;
        }

        public async Task<IEnumerable<Models.Domains.Task>> GetAllByCompanyAndUrgencyLevelWithLimitAsync( int companyId, TaskUrgencyLevel urgencyLevel, int offset, int count ) {
			String sp = "spSelectTasksByCompanyAndUrgencyLevelWithLimit";

			var result = await _connection.QueryAsync<Models.Domains.Task>(sp, new { CompanyId = companyId, UrgencyLevel = urgencyLevel.ToString(), offset, count }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

        public async Task<IEnumerable<Models.Domains.Task>> GetAllByCompanyWithCategoryAndUrgencyLevelLimitAsync(int companyId, TaskCategories category, TaskUrgencyLevel urgencyLevel, int offset, int count)
        {
			String sp = "spSelectTasksByCompanyWithCategoryAndUrgencyLevelLimit";


            var result = await _connection.QueryAsync<Models.Domains.Task>(sp, new { CompanyId = companyId, Category = category.ToString(), UrgencyLevel = urgencyLevel.ToString(), offset, count }, commandType: CommandType.StoredProcedure);

            if (result == null)
            {
                throw new Exception("No data found");
            }

            return result;
        }

        public async Task<IEnumerable<Models.Domains.Task>> GetAllByCompanyWithLimitAsync( int companyId, int offset, int count ) {
            
            String sp = "spSelectTasksByCompanyIdWithStatus";

			var result = await _connection.QueryAsync<Models.Domains.Task>(sp, new { CompanyId = companyId, offset, count }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
        }

        public async Task<IEnumerable<Models.Domains.Task>> GetAllByUrgencyLevelWithLimitAsync( TaskUrgencyLevel urgencyLevel, int offset, int count ) {
			String sp = "spSelectTasksByUrgencyLevelWithLimit";

			var result = await _connection.QueryAsync<Models.Domains.Task>(sp, new { UrgencyLevel = urgencyLevel.ToString(), offset, count }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
		}

		public async Task<Models.Domains.Task> GetByIdAsync( int id ) {
			String sp = "spSelectTaskByIdWithStatus";
            //spSelectTaskById
            var result = await _connection.QueryAsync<Models.Domains.Task>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);
			
			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No task was found" );
		}

		public async Task<Models.Domains.Task> UpdateAsync( Models.Domains.Task task ) {
			String sp = "spUpdateTaskById";

			var result = await _connection.QueryAsync<Models.Domains.Task>(sp, task, commandType: CommandType.StoredProcedure);

			if ( result != null && result.Count() > 0 ) {
				return result.First();
			}

			throw new Exception( "No task was updated" );
		}
	}
}
