using backend.Models.Domains;
using backend.Repositories.Interfaces;
using backend.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.test.Repositories
{
    public class RepositoryTaskResourceTest
    {
        private readonly IDbConnection _connection;
        private readonly IRepositoryTaskResource _repository;
        public RepositoryTaskResourceTest()
        {
            _connection = new MySqlConnection( "Server=db-mysql-blr1-08473-do-user-14661818-0.c.db.ondigitalocean.com;Database=new_erp;Uid=doadmin;Pwd=AVNS_VXDCh37l8j_R6GxtxAI;Port=25060" );
            _connection.Open();
            _repository = new RepositoryTaskResource(_connection);
        }

        [Fact]
        public async void Test_CreateAsync()
        {
            Models.Domains.TaskResource? result = null;
            try
            {
                result = await _repository.CreateAsync(new Models.Domains.TaskResource
                {
                    TaskId = 2,
                    URL = "taskresource2.jpg",
                    Category = TaskResourceCategory.Document.ToString(),
                    Comments = "Task Resource test comment",
                    LastUpdatedBy = 1

                });
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_GetAllByTaskIdAndURLAsync()
        {
            IEnumerable<Models.Domains.TaskResource>? result = null;
            try
            {
                result = await _repository.GetByTaskIdAndURLAsync(2, "taskresource2.jpg");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_GetAllByTaskIdAsync()
        {
            IEnumerable<Models.Domains.TaskResource>? result = null;
            try
            {
                result = await _repository.GetAllByTaskIdAsync(2);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.NotNull(result);
        }
    }
}
