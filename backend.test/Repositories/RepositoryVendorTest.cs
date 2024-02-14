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
	public class RepositoryVendorTest {
		private readonly IDbConnection _connection;
		private readonly IRepositoryVendor _repository;

		public RepositoryVendorTest() {
			_connection = new MySqlConnection( "Server=localhost;Database=new_erp;Uid=root;Pwd=BlackDragon321@b;Port=3306" );
			_connection.Open();
			_repository = new RepositoryVendor( _connection );
		}

		[Fact]
		public async void Test_CreateAsync() {
			Vendor? result = null;

			try {
				result = await _repository.CreateAsync( new Vendor {
					Name = "Test Vendor",
					Address = "Test Address",
					Email = "",
					Phone01 = "",
					Phone02 = "",
					Comments = "",
					CompanyId = 1,
					LastUpdatedBy = 1,
					VendorRegistrationNumber = ""

				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByCompanyWithLimitAsync() {
			IEnumerable<Vendor>? result = null;

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
			Vendor? result = null;

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
			Vendor? result = null;

			try {
				result = await _repository.UpdateAsync( new Vendor {
					Id = 1,
					Name = "Lol Vendor",
					Address = "Lol Address",
					Email = "",
					Phone01 = "",
					Phone02 = "",
					Comments = "",
					CompanyId = 1,
					LastUpdatedBy = 1,
					VendorRegistrationNumber = ""
				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}
	}
}

