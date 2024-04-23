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

        public async Task<ProjectService> CreateAsync(Models.Domains.ProjectService projectService)
        {
            string sp = "spInsertProjectService";

            var result = await _connection.QueryAsync<Models.Domains.ProjectService>(sp, projectService, commandType: CommandType.StoredProcedure);

            if (result != null && result.Count()>0)
            {
                return result.First();
            }

            throw new Exception("No any Project Service created");
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectService>> GetAllByProjectIdWithLimitAsync(int projectId, int offset, int count)
        {
            string sp = "spSelectProjectServiceByProjectIdWithLimit";

            var result = await _connection.QueryAsync<Models.Domains.ProjectService>(sp, new { ProjectId = projectId, offset, count }, commandType: CommandType.StoredProcedure);
            if(result == null)
            {
                throw new Exception( "No data Found");
            }
            return result;
        }


        public async Task<ProjectService> GetByIdAsync(int id)
        {
            string sp = "spSelectProjectServiceById";

            var result = await _connection.QueryAsync<Models.Domains.ProjectService>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);

            if (result!=null && result.Count()>0)
            {
                return result.First();
            }

            throw new Exception( "No any Project Service Found");

        }

        public async Task<ProjectService> UpdateAsync(Models.Domains.ProjectService projectService)
        {
            string sp = "spUpdateProjectServiceById";

            var result = await _connection.QueryAsync<Models.Domains.ProjectService>(sp, projectService, commandType: CommandType.StoredProcedure);

            if(result!=null && result.Count() > 0)
            {
                return result.First();
            }

            throw new Exception( "No any Project Service Updataed");
        }

        public async Task<IEnumerable<ProjectService>> GetAllProjectsByPendingStatusWithLimitAsync(int companyId, ProjectService.ProjectServiceStatus status, int offset, int count)
        {
            String sp = "spSelectProjectServiceByCompanyIdAndStatus";

            var result = await _connection.QueryAsync<Models.Domains.ProjectService>(sp, new { CompanyId = companyId, Status = status.ToString(), offset, count }, commandType: CommandType.StoredProcedure);
            if (result == null)
            {
                throw new Exception("No data Found");
            }
            return result;
        }
    }
}
