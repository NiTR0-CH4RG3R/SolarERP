using backend.Models.Domains;
using backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace backend.Repositories
{
    public class RepositoryProjectResource:IRepositoryProjectResource
    {
        private readonly IDbConnection _connection;

        public RepositoryProjectResource(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ProjectResource> CreateAsync(ProjectResource projectResource)
        {
            String sp = "spInsertProjectResource";

            var result = await _connection.QueryAsync<ProjectResource>(sp, projectResource, commandType: CommandType.StoredProcedure);

            if (result != null && result.Count() > 0)
            {
                return result.First();
            }

            throw new Exception("No project resource was created");
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectResource> GetByIdAsync(int id)
        {
            string sp = "spSelectProjectResourcebyId";

            var result = await _connection.QueryAsync<Models.Domains.ProjectResource>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);

            if (result != null && result.Count() > 0)
            {
                return result.First();
            }

            throw new NotImplementedException("No any Project Resource Found");

        }

        public async Task<ProjectResource> UpdateAsync(ProjectResource projectResource)
        {
            string sp = "spUpdateProjectResourceById";

            var result = await _connection.QueryAsync<Models.Domains.ProjectResource>(sp, projectResource, commandType: CommandType.StoredProcedure);

            if (result != null && result.Count() > 0)
            {
                return result.First();
            }

            throw new NotImplementedException("No any Project Resource Updataed");
        }

    }
}
