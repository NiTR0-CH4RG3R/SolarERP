using backend.Repositories.Interfaces;
using System.Data;
using Dapper;
using System.Security.Policy;
using backend.Models.Domains;

namespace backend.Repositories
{
    public class RepositoryTaskStatus : IRepositoryTaskStatus
    {
        private readonly IDbConnection _connection;

        public RepositoryTaskStatus(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<Models.Domains.TaskStatus> CreateAsync(Models.Domains.TaskStatus taskstatus)
        {
            String sp = "spInsertTaskStatus";

            var result = await _connection.QueryAsync<Models.Domains.TaskStatus>(sp, taskstatus, commandType: CommandType.StoredProcedure);

            if (result != null && result.Count() > 0)
            {
                return result.First();
            }

            throw new Exception("No task status was created");
        }

        public async Task<bool> DeleteById(int taskId)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Domains.TaskStatus> GetByIdAsync(int id)
        {
            String sp = "spSelectTaskStatusById";

            var result = await _connection.QueryAsync<Models.Domains.TaskStatus>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);

            if (result == null || result.Count() == 0)
            {
                throw new Exception("No data found");
            }

            return result.First();
        }
         
        public async Task<IEnumerable<Models.Domains.TaskStatus>> GetAllByTaskIdWithLimitAsync(int taskId, int offset, int count)
        {
            String sp = "spSelectTaskStatusByTaskIdWithLimit";

            var result = await _connection.QueryAsync<Models.Domains.TaskStatus>(sp, new { TaskId = taskId, offset, count }, commandType: CommandType.StoredProcedure);

            if (result == null)
            {
                throw new Exception("No data found");
            }

            return result;
        }

        public async Task<Models.Domains.TaskStatus> UpdateAsync(Models.Domains.TaskStatus taskstatus)
        {
            String sp = "spUpdateTaskStatusById";
            var result = await _connection.QueryAsync<Models.Domains.TaskStatus>(sp, taskstatus, commandType: CommandType.StoredProcedure);
            if (result == null)
            {
                throw new Exception("No task status was updated");
            }

            return result.First();
        }
    }
}
