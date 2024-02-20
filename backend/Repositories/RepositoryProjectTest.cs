using backend.Models.Domains;
using backend.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace backend.Repositories
{
    public class RepositoryProjectTest : IRepositoryProjectTest
    {
        private readonly IDbConnection _connection;

        public RepositoryProjectTest(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ProjectTest> CreateAsync(ProjectTest projectTest)
        {
            string sp = "spInsertProjectTest";

            var result = await _connection.QueryAsync<Models.Domains.ProjectTest>(sp, projectTest, commandType: CommandType.StoredProcedure);

            if(result != null && result.Count()>0) { 
                return result.First();
            }

            throw new NotImplementedException("No any Project Test Created");
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectTest>> GetAllByProjectIdWithLimitAsync(int projectId, int offset, int count)
        {
            string sp = "spSelectProjectTestByProjectIdWithLimit";

            var result = await _connection.QueryAsync<Models.Domains.ProjectTest>(sp, new { ProjectId = projectId, offset, count }, commandType: CommandType.StoredProcedure);

            if(result == null)
            {
                throw new NotImplementedException("No data Found");
            }
            return result;
        }

        public async Task<ProjectTest> GetByIdAsync(int id)
        {
            string sp = "spSelectProjectTestById";

            var result = await _connection.QueryAsync<Models.Domains.ProjectTest>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);

            if(result!=null && result.Count()>0)
            {
                return result.First();
            }

            throw new NotImplementedException("No any Project Test Found");
        }

        public async Task<ProjectTest> UpdateAsync(ProjectTest projectTest)
        {
            string sp = "spUpdateProjectTestById";

            var result = await _connection.QueryAsync<Models.Domains.ProjectTest>(sp, projectTest, commandType:CommandType.StoredProcedure);

            if (result!=null && result.Count()>0) {
                return result.First();
            }

            throw new NotImplementedException("No any ProjectTest Updated");
        }
    }
}
