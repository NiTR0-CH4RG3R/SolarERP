using backend.Models.Domains;
using backend.Repositories;
using backend.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.test.Repositories
{
    public class RepositoryProjectServiceTest
    {
        private readonly IDbConnection _connection;
        private readonly IRepositoryProjectService _repository;

        public RepositoryProjectServiceTest()
        {
            _connection = new MySqlConnection("Server=db-mysql-blr1-08473-do-user-14661818-0.c.db.ondigitalocean.com;Database=new_erp;Uid=doadmin;Pwd=AVNS_VXDCh37l8j_R6GxtxAI;Port=25060" );
            _connection.Open();
            _repository = new RepositoryProjectService(_connection);
        }

        [Fact]
        public async void Test_CreateAsync()
        {
            ProjectService? result = null;
            try
            {
                result = await _repository.CreateAsync(new Models.Domains.ProjectService {
                    ProjectId = 2,
                    PlannedDate = DateTime.Now,
                    Status = ProjectService.ProjectServiceStatus.Pending.ToString(),
                    ConductedBy = 9,
                    ConductedDate = DateTime.Now,
                    Priority = ProjectService.ProjectServicePriority.Low.ToString(),
                    LastUpdatedDateTime = DateTime.Now,
                });
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_GetByIdAsync()
        {
            Models.Domains.ProjectService? result = null;

            try
            {
                result = await _repository.GetByIdAsync(2);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_GetAllByProjectIdWithLimitAsync()
        {
            IEnumerable<Models.Domains.ProjectService>? result = null;
            try
            {
                result = await _repository.GetAllByProjectIdWithLimitAsync(2, 0, 10);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_UpdateAsync()
        {
            Models.Domains.ProjectService? result = null;

            try
            {
                result = await _repository.UpdateAsync(new Models.Domains.ProjectService { 
                    Id = 2,
                    ProjectId = 2,
                    PlannedDate = DateTime.Now,
                    Status = ProjectService.ProjectServiceStatus.Pending.ToString(),
                    ConductedBy = 9,
                    ConductedDate = DateTime.Now,
                    Priority = ProjectService.ProjectServicePriority.Low.ToString(),
                    LastUpdatedDateTime = DateTime.Now,
                });
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.NotNull(result);
        }
    }
}
