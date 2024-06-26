﻿using backend.Models.Domains;
using backend.Repositories;
using backend.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace backend.test.Repositories {
	public class RepositoryCompanyTest {

		private readonly IDbConnection _connection;
		private readonly IRepositoryCompany _repository;

		public RepositoryCompanyTest() {
			_connection = new MySqlConnection( "Server=db-mysql-blr1-08473-do-user-14661818-0.c.db.ondigitalocean.com;Database=new_erp;Uid=doadmin;Pwd=AVNS_VXDCh37l8j_R6GxtxAI;Port=25060" );
			_connection.Open();
			_repository = new RepositoryCompany(_connection);
		}

		[Fact]
		public async void Test_CreateAsync() {
			Company? result = null;

			try {
				result = await _repository.CreateAsync( new Company {
					Name = "Test Company",
					Address = "Test Address"
				} );
			}
			catch (Exception ex) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllAsync() {
			IEnumerable<Company>? result = null;

			try {
				result = await _repository.GetAllAsync( );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetByIdAsync() {
			Company? result = null;

			try {
				result = await _repository.GetByIdAsync( 1 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_UpdateAsync() {
			Company? result = null;

			try {
				result = await _repository.UpdateAsync( new Company {
					Id = 1,
					Name = "Lol Company",
					Address = "Lol Address"
				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}
	}
}
