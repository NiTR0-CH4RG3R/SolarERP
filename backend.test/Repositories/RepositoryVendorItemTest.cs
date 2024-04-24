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
	public class RepositoryVendorItemTest {
		private readonly IDbConnection _connection;
		private readonly IRepositoryVendorItem _repository;

		public RepositoryVendorItemTest() {
			_connection = new MySqlConnection( "Server=db-mysql-blr1-08473-do-user-14661818-0.c.db.ondigitalocean.com;Database=new_erp;Uid=doadmin;Pwd=AVNS_VXDCh37l8j_R6GxtxAI;Port=25060" );
			_connection.Open();
			_repository = new RepositoryVendorItem( _connection );
		}

		[Fact]
		public async void Test_CreateAsync() {
			VendorItem? result = null;

			try {
				result = await _repository.CreateAsync( new VendorItem {
					ProductName = "Test Product",
					ProductCode = "12",
					Price = 100,
					WarrantyDuration = "23",
					Capacity = 1,
					Brand = "Test Brand",
					Comments = "Test Comments",
					VendorId = 1,
					LastUpdatedBy = 1
				} ); 
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByCompanyWithLimitAsync() {
			IEnumerable<VendorItem>? result = null;

			try {
				result = await _repository.GetAllByCompanyWithLimitAsync( 1, 0, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByVendorWithLimitAsync() {
			IEnumerable<VendorItem>? result = null;

			try {
				result = await _repository.GetAllByVendorWithLimitAsync( 1, 0, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetByIdAsync() {
			VendorItem? result = null;

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
			VendorItem? result = null;

			try {
				result = await _repository.UpdateAsync( new VendorItem {
					Id = 1,
					ProductName = "Lol Product",
					ProductCode = "12",
					Price = 100,
					WarrantyDuration = "23",
					Capacity = 1,
					Brand = "Lol Brand",
					Comments = "Lol Comments",
					VendorId = 1,
					LastUpdatedBy = 1
				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}
	}
}
