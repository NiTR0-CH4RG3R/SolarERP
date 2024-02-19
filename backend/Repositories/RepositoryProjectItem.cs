using backend.Models.Domains;
using backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace backend.Repositories
{
    public class RepositoryProjectItem: IRepositoryProjectItem
    {
        private readonly IDbConnection _connection;

        public RepositoryProjectItem(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ProjectItem> CreateAsync(ProjectItem projectItem)
        {
            String sp = "spInsertProjectItem";

            var result = await _connection.QueryAsync<ProjectItem>(sp, projectItem, commandType: CommandType.StoredProcedure);

            if (result != null && result.Count() > 0)
            {
                return result.First();
            }

            throw new Exception("No project item was created");
        }

        public async Task<ProjectItem> GetByIdAsync(int id)
        {
            String sp = "spSelectProjectItemById";

            var result = await _connection.QueryAsync<ProjectItem>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);

            if (result != null && result.Count() > 0)
            {
                return result.First();
            }

            throw new Exception("No data found");
        }

        public async Task<ProjectItem> UpdateAsync(ProjectItem projectItem)
        {
            String sp = "spUpdateProjectItemById";

            var result = await _connection.QueryAsync<ProjectItem>(sp, projectItem, commandType: CommandType.StoredProcedure);

            if (result != null && result.Count() > 0)
            {
                return result.First();
            }

            throw new Exception("No project item was updated");
        }
    }
}

