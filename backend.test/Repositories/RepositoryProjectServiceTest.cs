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
            _connection = new MySqlConnection("Server=localhost;Database=new_erp;Uid=root;Pwd=19240;Port=3307");
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
        public async void Test_UpdateAsync()
        {
            Models.Domains.ProjectService? result = null;

            try
            {
                result = await _repository.UpdateAsync(new Models.Domains.ProjectService { 
                    Id = 2,
                    ProjectId = 2,
                    PlannedDate = DateTime.Now,
                    Status = ProjectService.ProjectServiceStatus.Done.ToString(),
                    ConductedBy = 9,
                    ConductedDate = DateTime.Now,
                    Priority = ProjectService.ProjectServicePriority.Urgent.ToString(),
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
