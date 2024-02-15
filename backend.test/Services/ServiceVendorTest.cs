using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using backend.Repositories;
using backend.Models.DTO.Vendor;

namespace backend.test.Services {
	public class ServiceVendorTest {
		private readonly IDbConnection _connection;
		private readonly IRepositoryParticipant _repositoryParticipant;
		private readonly IRepositorySystemUser _repositorySystemUser;
		private readonly IRepositoryVendor _repositoryVendor;
		private readonly ILogger<ServiceCustomer> _logger;
		private readonly IServiceVendor _serviceVendor;

		public ServiceVendorTest() {
			var serviceProvider = new ServiceCollection()
									.AddLogging()
									.BuildServiceProvider();

			var factory = serviceProvider.GetService<ILoggerFactory>();

			_logger = factory.CreateLogger<ServiceCustomer>();

			_connection = new MySqlConnection( "Server=localhost;Database=new_erp;Uid=root;Pwd=BlackDragon321@b;Port=3306" );
			_connection.Open();

			_repositoryParticipant = new RepositoryParticipant( _connection );
			_repositorySystemUser = new RepositorySystemUser( _connection );
			_repositoryVendor = new RepositoryVendor( _connection );

			_serviceVendor = new ServiceVendor( _repositoryParticipant, _repositorySystemUser, _repositoryVendor, _logger );
		}

		[Fact]
		public async void Test_CreateAsync() {
			GetVendorDTO? result = null;

			try {
				result = await _serviceVendor.CreateAsync( 2, new AddVendorDTO {
					Name = "Test Name",
					Address = "Test Address",
					Email = "Test Email",
					Phone01 = "Test Phone01",
					Phone02 = "Test Phone02",
					VendorRegistrationNumber = "qTest VendorRegistrationNumber",
					Comments = "Test Comments",
				} );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}



		[Fact]
		public async void Test_GetAllAsync() {
			IEnumerable<GetVendorDTO>? result = null;

			try {
				result = await _serviceVendor.GetAllAsync( 2, 1, 10 );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}


		[Fact]
		public async void Test_GetByIdAsync() {
			GetVendorDTO? result = null;

			try {
				result = await _serviceVendor.GetByIdAsync( 1 );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetByIdAsync_ThrowsException() {
			await Assert.ThrowsAsync<Exception>( async () => {
				await _serviceVendor.GetByIdAsync( 2 );
			} );
		}

		[Fact]
		public async void Test_UpdateAsync() {
			GetVendorDTO? result = null;

			try {
				result = await _serviceVendor.UpdateAsync( 2, 1, new AddVendorDTO {
					Name = "Test Name",
					Address = "Test Address",
					Email = "Test Email",
					Phone01 = "Test Phone01",
					Phone02 = "Test Phone02",
					VendorRegistrationNumber = "1Test VendorRegistrationNumber",
					Comments = "Test Comments",
				} );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}
	}
}	
