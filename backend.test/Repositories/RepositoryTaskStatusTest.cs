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
    public class RepositoryTaskStatusTest
    {
        private readonly IDbConnection _connection;
        private readonly IRepositoryTaskStatus _repository;
        public RepositoryTaskStatusTest()
        {
            _connection = new MySqlConnection("Server=db-mysql-blr1-08473-do-user-14661818-0.c.db.ondigitalocean.com;Database=new_erp;Uid=doadmin;Pwd=AVNS_VXDCh37l8j_R6GxtxAI;Port=25060" );
            _connection.Open();
            _repository = new RepositoryTaskStatus(_connection);
        }

        [Fact]
        public async void Test_CreateAsync()
        {
            Models.Domains.TaskStatus? result = null;
            try
            {
                result = await _repository.CreateAsync(new Models.Domains.TaskStatus
                {
                    TaskId = 3,
                    Status = TaskStatusCategory.Active.ToString(),
                    Comments = "This is active status test comment",
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
        public async void Test_GetByIdAsync()
        {
            Models.Domains.TaskStatus? result = null;
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
        public async void GetAllByTaskIdWithLimitAsync()
        {
            IEnumerable<Models.Domains.TaskStatus>? result = null;
            try
            {
                result = await _repository.GetAllByTaskIdWithLimitAsync(2, 0, 10);
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
            Models.Domains.TaskStatus? result = null;
            try
            {
                result = await _repository.UpdateAsync(new Models.Domains.TaskStatus
                {
                    Id =11,
                    TaskId = 4,
                    Status = TaskStatusCategory.Invalid.ToString(),
                    Comments = "This is invalid status updated comment",
                    LastUpdatedBy = 3
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

