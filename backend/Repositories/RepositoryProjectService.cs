using backend.Models.Domains;
using backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace backend.Repositories
{
    public class RepositoryProjectService : IRepositoryProjectService
    {
        private readonly IDbConnection _connection;

        public RepositoryProjectService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ProjectService> CreateAsync(ProjectService projectService)
        {
            string sp = "spInsertProjectService";

            var result = await _connection.QueryAsync<Models.Domains.ProjectService>(sp, projectService, commandType: CommandType.StoredProcedure);

            if (result != null && result.Count()>0)
            {
                return result.First();
            }

            throw new NotImplementedException("No any Project Service created");
        }

        public async Task<ProjectService> GetByIdAsync(int id)
        {
            string sp = "spSelectProjectServicebyId";

            var result = await _connection.QueryAsync<Models.Domains.ProjectService>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);

            if (result!=null && result.Count()>0)
            {
                return result.First();
            }

            throw new NotImplementedException("No any Project Service Found");

        }
    }
}
