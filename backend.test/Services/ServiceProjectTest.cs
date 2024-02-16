using backend.Repositories.Interfaces;
using backend.Repositories;
using backend.Services.Interfaces;
using backend.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Models.DTO.Project;
using backend.Models.Domains;

namespace backend.test.Services {
	public class ServiceProjectTest {

		private readonly IDbConnection _connection;
		private readonly IRepositoryParticipant _repositoryParticipant;
		private readonly IRepositorySystemUser _repositorySystemUser;
		private readonly IRepositoryProject _repositoryProject;
		private readonly ILogger<ServiceCustomer> _logger;
		private readonly IServiceProject _serviceProject;

		public ServiceProjectTest() {
			var serviceProvider = new ServiceCollection()
									.AddLogging()
									.BuildServiceProvider();

			var factory = serviceProvider.GetService<ILoggerFactory>();

			_logger = factory.CreateLogger<ServiceCustomer>();

			_connection = new MySqlConnection( "Server=localhost;Database=new_erp;Uid=root;Pwd=BlackDragon321@b;Port=3306" );
			_connection.Open();

			_repositoryParticipant = new RepositoryParticipant( _connection );
			_repositorySystemUser = new RepositorySystemUser( _connection );
			_repositoryProject = new RepositoryProject( _connection );

			_serviceProject = new ServiceProject( _repositoryParticipant, _repositorySystemUser, _repositoryProject, _logger );
		}

		[Fact]
		public async void CreateAsync() {
			// Arrange
			Int32 userId = 1;
			AddProjectDTO project = new AddProjectDTO {
				Description = "Test",
				CommissionDate = DateTime.Now,
				StartDate = DateTime.Now,
				Address = "Test",
				Comments = "Test",
				CoordinatorId = 1,
				CustomerId = 1,
				ElectricityAccountNumber = "Test",
				ElectricityBoardArea	= "Test",
				ElectricityTariffStructure = "Test",
				EstimatedCost = 1,
				LocationCoordinates = "Test",
				ProjectIdentificationNumber = "Test",
				ReferencedBy = 1,
				Status = ProjectStatus.Active.ToString(),
				SystemWarrantyPeriod = "Test"
				
			};

			// Act

			GetProjectDTO? result = null;
			try {

				result = await _serviceProject.CreateAsync( userId, project );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			// Assert
			Assert.NotNull( result );
		}

		[Fact]
		public async void GetByIdAsync() {
			// Arrange
			Int32 userId = 1;
			Int32 id = 1;

			// Act
			GetProjectDTO? result = null;
			try {
				result = await _serviceProject.GetByIdAsync( id );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			// Assert
			Assert.NotNull( result );
		}

		[Fact]
		public async void GetAllAsync() {
			// Arrange
			Int32 userId = 1;

			// Act
			IEnumerable<GetProjectDTO>? result = null;
			try {
				result = await _serviceProject.GetAllWithLimitAsync( userId, 1, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			// Assert
			Assert.NotNull( result );
		}

		[Fact]
		public async void UpdateAsync() {
			// Arrange
			Int32 userId = 1;
			Int32 id = 1;
			AddProjectDTO project = new AddProjectDTO {
				Description = "Test",
				CommissionDate = DateTime.Now,
				StartDate = DateTime.Now,
				Address = "Test",
				Comments = "Test",
				CoordinatorId = 1,
				CustomerId = 1,
				ElectricityAccountNumber = "Test",
				ElectricityBoardArea	= "Test",
				ElectricityTariffStructure = "Test",
				EstimatedCost = 1,
				LocationCoordinates = "Test",
				ProjectIdentificationNumber = "Test",
				ReferencedBy = 1,
				Status = ProjectStatus.Active.ToString(),
				SystemWarrantyPeriod = "Test"
			};

			// Act
			GetProjectDTO? result = null;
			try {
				result = await _serviceProject.UpdateAsync( userId, id, project );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			// Assert
			Assert.NotNull( result );
		}



	}
}
