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
    public class RepositoryProjectTestTest
    {
        private readonly IDbConnection  _connection;
        private readonly IRepositoryProjectTest _repository;
        public RepositoryProjectTestTest()
        {
            _connection = new MySqlConnection("Server=db-mysql-blr1-08473-do-user-14661818-0.c.db.ondigitalocean.com;Database=new_erp;Uid=doadmin;Pwd=AVNS_VXDCh37l8j_R6GxtxAI;Port=25060" );
            _connection.Open();
            _repository = new backend.Repositories.RepositoryProjectTest(_connection);
        }

        [Fact]
        public async void Test_CreateAsync()
        {
            Models.Domains.ProjectTest? result = null;

            try
            {
                result = await _repository.CreateAsync(new Models.Domains.ProjectTest{
                    ProjectId = 1,
                    Name = "Test",
                    Passed = 0,
                    ConductedBy = 3,
                    ConductedDate = DateTime.Now,
                    LastUpdatedDateTime = DateTime.Now,
                }); ;
            }
            catch( Exception ex )
            {
                Assert.Fail(ex.Message);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_GetById()
        {
            Models.Domains.ProjectTest? result = null;

            try
            {
                result = await _repository.GetByIdAsync(2);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_GetAllByProjectIdWithLimitAsync()
        {
            IEnumerable<Models.Domains.ProjectTest>? result = null;
            try
            {
                result = await _repository.GetAllByProjectIdWithLimitAsync(1, 10, 0);
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
            Models.Domains.ProjectTest? result = null;

            try
            {
                result = await _repository.UpdateAsync(new Models.Domains.ProjectTest {
                    Id = 2,
                    ProjectId = 1,
                    Name = "Unit Test",
                    Passed = 1,
                    ConductedBy = 3,
                    ConductedDate = DateTime.Now,
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
