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
    public class RepositoryProjectResourceTest
    {
        private readonly IDbConnection _connection;
        private readonly IRepositoryProjectResource _repository;

        public RepositoryProjectResourceTest()
        {
            _connection = new MySqlConnection("Server=localhost;Database=new_erp;Uid=root;Pwd=123;Port=3306");
            _connection.Open();
            _repository = new RepositoryProjectResource(_connection);
        }

        [Fact]
        public async void Test_CreateAsync()
        {
            ProjectResource? result = null;

            try
            {
                result = await _repository.CreateAsync(new ProjectResource
                {
                    LastUpdatedBy=1,
                    Comments = "Test Comments",
                    ProjectId = 1,
                    URL="addd",
                    Category="Image"
                    
                    
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
            ProjectResource? result = null;

            try
            {
                result = await _repository.GetByIdAsync(1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.NotNull(result);
        }


        [Fact]
        public async void Test_GetAllByProjectWithLimitAsync()
        {
            IEnumerable<ProjectResource>? result = null;
            try
            {
                result = await _repository.GetAllByProjectWithLimitAsync(1, 0, 10);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_UpdateAsync()
        {
            ProjectResource? result = null;

            try
            {
                result = await _repository.UpdateAsync(new ProjectResource
                {
                    Id = 1,
                    LastUpdatedBy = 1,
                    Comments = "LOL Comments",
                    ProjectId = 1,
                    URL = "fgrr",
                    Category = "Document"
                });
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.NotNull(result);
        }
    }
}
