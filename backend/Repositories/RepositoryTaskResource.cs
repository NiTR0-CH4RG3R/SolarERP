﻿using backend.Models.Domains;
using backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace backend.Repositories
{
    public class RepositoryTaskResource : IRepositoryTaskResource
    {
        private readonly IDbConnection _connection;

        public RepositoryTaskResource(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<TaskResource> CreateAsync(TaskResource taskresource)
        {
            String sp = "spInsertTaskResource";

            var result = await _connection.QueryAsync<TaskResource>(sp, taskresource, commandType: CommandType.StoredProcedure);

            if (result != null && result.Count() > 0)
            {
                return result.First();
            }

            throw new Exception("No task resource was created");
        }

        public async Task<bool> DeleteByTaskIdAndURLAsync(int taskId, string URL)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskResource>> GetByTaskIdAndURLAsync(int taskId, string url)
        {
            String sp = "spSelectTaskResourceByTaskIdAndURL";

			var result = await _connection.QueryAsync<Models.Domains.TaskResource>(sp, new { TaskId = taskId, URL = url }, commandType: CommandType.StoredProcedure);

			if ( result == null ) {
				throw new Exception( "No data found" );
			}

			return result;
        }

        public async Task<IEnumerable<TaskResource>> GetAllByTaskIdAsync(int taskId)
        {
            String sp = "spSelectTaskResourcesByTaskId";

            var result = await _connection.QueryAsync<Models.Domains.TaskResource>(sp, new { TaskId = taskId }, commandType: CommandType.StoredProcedure);

            if (result == null)
            {
                throw new Exception("No data found");
            }

            return result;
        }
    }
}
