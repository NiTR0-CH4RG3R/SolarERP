using backend.Repositories.Interfaces;
using backend.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Models.Domains;

namespace backend.test.Repositories {
	public class RepositorySystemUserTest {
		private readonly IDbConnection _connection;
		private readonly IRepositorySystemUser _repository;

		public RepositorySystemUserTest() {
			_connection = new MySqlConnection( "Server=localhost;Database=new_erp;Uid=root;Pwd=BlackDragon321@b;Port=3306" );
			_connection.Open();
			_repository = new RepositorySystemUser( _connection );
		}

		[Fact]
		public async void Test_CreateAsync() {
			SystemUser? result = null;

			try {
				result = await _repository.CreateAsync( new SystemUser {
					Id = 3,
					Role = SystemUserRoles.Admin.ToString(),
					Username = "ADMIN2",
					Password = "ADMINPWD",
					ProfilePicture = "Test Profile Picture",
				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByCompanyAsync() {
			IEnumerable<SystemUser>? result = null;

			try {
				result = await _repository.GetAllByCompanyAsync( 1 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByCompanyWithLimitAsync() {
			IEnumerable<SystemUser>? result = null;

			try {
				result = await _repository.GetAllByCompanyWithLimitAsync( 1, 0, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetByIdAsync() {
			SystemUser? result = null;

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
			SystemUser? result = null;

			try {
				result = await _repository.UpdateAsync( new SystemUser {
					Id = 2,
					Role = SystemUserRoles.User.ToString(),
					Username = "fff",
					Password = "123",
					ProfilePicture = "Test Profile Picture",
				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		// [Fact]
		public async void Test_DeleteAsync() {
			bool result = false;

			try {
				result = await _repository.DeleteAsync( 1 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.True( result );
		}

		[Fact]
		public async void Test_GetByUsernameAndPasswordAsync() {
			SystemUser? result = null;

			try {
				result = await _repository.GetByUsernameAndPasswordAsync( "ADMIN2", "ADMINPWD" );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}
	}
}
