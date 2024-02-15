﻿using backend.Models.Domains;
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
            _connection = new MySqlConnection( "Server=localhost;Database=new_erp;Uid=root;Pwd=BlackDragon321@b;Port=3306" );
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
                    Category = ResourceCategories.Document.ToString(),
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
                result = await _repository.GetAllByTaskIdAndURLAsync(2, "taskresource2.jpg", 0, 10);
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
                result = await _repository.GetAllByTaskIdAsync(2, 0, 10);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.NotNull(result);
        }
    }
}
