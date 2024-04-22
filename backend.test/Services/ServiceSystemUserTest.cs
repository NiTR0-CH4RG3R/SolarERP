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
using backend.Models.Domains;
using backend.Models.DTO.SystemUser;

namespace backend.test.Services {
	public class ServiceSystemUserTest {
		private readonly IDbConnection _connection;
		private readonly IRepositoryParticipant _repositoryParticipant;
		private readonly IRepositorySystemUser _repositorySystemUser;
		private readonly ILogger<ServiceCustomer> _logger;
		private readonly IServiceSystemUser _serviceSystemUser;

		public ServiceSystemUserTest() {
			var serviceProvider = new ServiceCollection()
									.AddLogging()
									.BuildServiceProvider();

			var factory = serviceProvider.GetService<ILoggerFactory>();

			_logger = factory.CreateLogger<ServiceCustomer>();

			_connection = new MySqlConnection( "Server=localhost;Database=new_erp;Uid=root;Pwd=BlackDragon321@b;Port=3306" );
			_connection.Open();

			_repositoryParticipant = new RepositoryParticipant( _connection );
			_repositorySystemUser = new RepositorySystemUser( _connection );

			// Create a dummy mapper
			//_serviceCustomer = new ServiceCustomer( _repositoryParticipant, _repositorySystemUser, _logger );


			_serviceSystemUser = new ServiceSystemUser( _repositoryParticipant, _repositorySystemUser, _logger,  new AutoMapper.Mapper(MapperConfig.Configure()));
		}

		[Fact]
		public async void Test_CreateAsync() {
			GetSystemUserDTO? result = null;

			try {
				result = await _serviceSystemUser.CreateAsync( 2, new AddSystemUserDTO {
					FirstName = "Test First Name",
					LastName = "Test Last Name",
					Email = "Test Email",
					Phone01 = "Test Phone01",
					Phone02 = "Test Phone02",
					Address = "Test Address",
					CustomerRegistrationNumber = null,
					Role = SystemUserRoles.Admin.ToString(),
					Profession = "Test Profession",
					Comments = "Test Comments",
					Username = "hello",
					Password = "Twerword",
					ProfilePicture = "Test Profile Picture"
				} ) ;
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByCompanyAsync() {
			IEnumerable<GetSystemUserDTO>? result = null;

			try {
				result = await _serviceSystemUser.GetAllAsync( 2 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByCompanyWithLimitAsync() {
			IEnumerable<GetSystemUserDTO>? result = null;

			try {
				result = await _serviceSystemUser.GetAllWithLimitAsync( 2, 1, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetByIdAsync() {
			GetSystemUserDTO? result = null;

			try {
				result = await _serviceSystemUser.GetByIdAsync( 2 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_UpdateAsync() {
			GetSystemUserDTO? result = null;

			try {
				result = await _serviceSystemUser.UpdateAsync( 3, 2, new AddSystemUserDTO {
					FirstName = "Test First Name",
					LastName = "Test Last Name",
					Email = "Test Email",
					Phone01 = "Test Phone01",
					Phone02 = "Test Phone02",
					Address = "Test Address",
					CustomerRegistrationNumber = null,
					Role = SystemUserRoles.Admin.ToString(),
					Profession = "Test Profession",
					Comments = "Test Comments",
					Username = "hello1324",
					Password = "Twerword",
					ProfilePicture = "Test Profile Picture"
				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}
	}
}
