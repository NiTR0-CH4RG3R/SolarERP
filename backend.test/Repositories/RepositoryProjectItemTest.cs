﻿using backend.Models.Domains;
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
    public class RepositoryProjectItemTest
    {
        private readonly IDbConnection _connection;
        private readonly IRepositoryProjectItem _repository;

        public RepositoryProjectItemTest()
        {
            _connection = new MySqlConnection("Server=localhost;Database=new_erp;Uid=root;Pwd=123;Port=3306");
            _connection.Open();
            _repository = new RepositoryProjectItem(_connection);
        }

        [Fact]
        public async void Test_CreateAsync()
        {
            ProjectItem? result = null;

            try
            {
                result = await _repository.CreateAsync(new ProjectItem
                {
                    ProjectId = 1,
                    VendorItemId = 1,
                    ModuleNo = "a1",
                    WarrantyDuration="5 yrs",
                    SerialNo="s1",
                    Comments="Test Comments",
                    LastUpdatedBy=1


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
            ProjectItem? result = null;

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
        public async void Test_UpdateAsync()
        {
            ProjectItem? result = null;

            try
            {
                result = await _repository.UpdateAsync(new ProjectItem
                {
                    Id = 1,
                    ProjectId = 1,
                    VendorItemId = 1,
                    ModuleNo = "1",
                    WarrantyDuration = "4 yrs",
                    SerialNo = "s7",
                    Comments = "LOL Comments",
                    LastUpdatedBy = 1
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
