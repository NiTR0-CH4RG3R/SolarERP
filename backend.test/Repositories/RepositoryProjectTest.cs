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
	public class RepositoryProjectTest {
		private readonly IDbConnection _connection;
		private readonly IRepositoryProject _repository;

		public RepositoryProjectTest() {
			_connection = new MySqlConnection( "Server=localhost;Database=new_erp;Uid=root;Pwd=BlackDragon321@b;Port=3306" );
			_connection.Open();
			_repository = new RepositoryProject( _connection );
		}

		[Fact]
		public async void TestCreateAsync() {
			var project = new Project {
				CompanyId = 1,
				CommissionDate = null,
				LastUpdatedDateTime = null,
				LastUpdatedBy = 1,
				Address = "Test Address",
				SalesPerson = 1,
				Comments = "Test Comments",
				SystemWarrantyPeriod = "Test Warranty",
				LocationCoordinates = "Test Coordinates",
				ProjectIdentificationNumber = "Test Number",
				ElectricityAccountNumber = "Test Account",
				ElectricityBoardArea = "Test Area",
				ElectricityTariffStructure = "Test Structure",
				EstimatedCost = 1000,
				ReferencedBy = 1,
				CustomerId = 1,
				CoordinatorId = 1,
				Description = "Test Description",
				StartDate = DateTime.Now,
				Id = null,
				Status = "Active"
			};

			Project? result = null;

			try {
				result = await _repository.CreateAsync( project );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void TestGetAllByCompanyWithLimitAsync() {
			IEnumerable<Project>? result = null;

			try {
				result = await _repository.GetAllByCompanyWithLimitAsync( 1, 0, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void TestGetByIdAsync() {
			Project? result = null;

			try {
				result = await _repository.GetByIdAsync( 1 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void TestUpdateAsync() {
			var project = new Project {
				CompanyId = 1,
				CommissionDate = null,
				LastUpdatedDateTime = null,
				LastUpdatedBy = 1,
				Address = "Test Address",
				SalesPerson = 1,
				Comments = "Test Comments",
				SystemWarrantyPeriod = "Test Warranty",
				LocationCoordinates = "Test Coordinates",
				ProjectIdentificationNumber = "Test Number",
				ElectricityAccountNumber = "Test Account",
				ElectricityBoardArea = "Test Area",
				ElectricityTariffStructure = "Test Structure",
				EstimatedCost = 1000,
				ReferencedBy = 1,
				CustomerId = 1,
				CoordinatorId = 1,
				Description = "Test Description",
				StartDate = DateTime.Now,
				Id = 1,
				Status = "Active"
			};

			Project? result = null;

			try {
				result = await _repository.UpdateAsync( project );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}
	}
}
