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

namespace backend.test.Repositories {
	public class RepositoryTaskTest {
		private readonly IDbConnection _connection;
		private readonly IRepositoryTask _repository;
		public RepositoryTaskTest() {
			_connection = new MySqlConnection( "Server=db-mysql-blr1-08473-do-user-14661818-0.c.db.ondigitalocean.com;Database=new_erp;Uid=doadmin;Pwd=AVNS_VXDCh37l8j_R6GxtxAI;Port=25060" );
			_connection.Open();
			_repository = new RepositoryTask( _connection );
		}

		[Fact]
		public async void Test_CreateAsync() {
			Models.Domains.Task? result = null;
			try {
				result = await _repository.CreateAsync( new Models.Domains.Task {
					CompanyId = 1,
					AssignedTo = 1,
					CallBackNumber = "1234567890",
					Comments = "Test Comments",
					Category = TaskCategories.Inquiry.ToString(),
					Description = "Test Description",
					LastUpdatedBy = 1,
					ProjectId = null,
					RequestedBy = 1,
					UrgencyLevel = TaskUrgencyLevel.Critical.ToString()
				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByAssignedToWithLimitAsync() {
			IEnumerable<Models.Domains.Task>? result = null;
			try {
				result = await _repository.GetAllByAssignedToWithLimitAsync( 1, 0, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByCompanyWithLimitAsync() {
			IEnumerable<Models.Domains.Task>? result = null;
			try {
				result = await _repository.GetAllByCompanyWithLimitAsync( 1, 0, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByUrgencyLevelWithLimitAsync() {
			IEnumerable<Models.Domains.Task>? result = null;
			try {
				result = await _repository.GetAllByUrgencyLevelWithLimitAsync( TaskUrgencyLevel.Critical, 0, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByCompanyAndUrgencyLevelWithLimitAsync() {
			IEnumerable<Models.Domains.Task>? result = null;
			try {
				result = await _repository.GetAllByCompanyAndUrgencyLevelWithLimitAsync( 1, TaskUrgencyLevel.Critical, 0, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

        [Fact]
        public async void Test_GetAllByCompanyAndCategoryWithLimitAsync()
        {
            IEnumerable<Models.Domains.Task>? result = null;
            try
            {
                result = await _repository.GetAllByCompanyAndCategoryWithLimitAsync(1, TaskCategories.Inquiry, 0, 5);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.NotNull(result);
        }

        [Fact]
		public async void Test_GetByIdAsync() {
			Models.Domains.Task? result = null;
			try {
				result = await _repository.GetByIdAsync( 2 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_UpdateAsync() {
			Models.Domains.Task? result = null;
			try {
				result = await _repository.UpdateAsync( new Models.Domains.Task {
					Id = 2,
					CompanyId = 1,
					AssignedTo = 1,
					CallBackNumber = "1234567890",
					Comments = "Test Comments",
					Category = TaskCategories.Inquiry.ToString(),
					Description = "Test Description",
					LastUpdatedBy = 1,
					ProjectId = null,
					RequestedBy = 1,
					UrgencyLevel = TaskUrgencyLevel.Critical.ToString()
				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

	}
}
