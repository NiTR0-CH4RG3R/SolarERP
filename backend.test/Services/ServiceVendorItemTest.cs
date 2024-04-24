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
using backend.Models.DTO.VendorItem;

namespace backend.test.Services {
	public class ServiceVendorItemTest {
		private readonly IDbConnection _connection;
		private readonly IRepositoryParticipant _repositoryParticipant;
		private readonly IRepositorySystemUser _repositorySystemUser;
		private readonly IRepositoryVendor _repositoryVendor;
		private readonly IRepositoryVendorItem _repositoryVendorItem;
		private readonly ILogger<ServiceCustomer> _logger;
		private readonly IServiceVendorItem _serviceVendorItem;

		public ServiceVendorItemTest() {
			var serviceProvider = new ServiceCollection()
									.AddLogging()
									.BuildServiceProvider();

			var factory = serviceProvider.GetService<ILoggerFactory>();

			_logger = factory.CreateLogger<ServiceCustomer>();

			_connection = new MySqlConnection( "Server=db-mysql-blr1-08473-do-user-14661818-0.c.db.ondigitalocean.com;Database=new_erp;Uid=doadmin;Pwd=AVNS_VXDCh37l8j_R6GxtxAI;Port=25060" );
			_connection.Open();

			_repositoryParticipant = new RepositoryParticipant( _connection );
			_repositorySystemUser = new RepositorySystemUser( _connection );
			_repositoryVendor = new RepositoryVendor( _connection );
			_repositoryVendorItem = new RepositoryVendorItem( _connection );

			_serviceVendorItem = new ServiceVendorItem( _repositoryParticipant, _repositorySystemUser, _repositoryVendor, _repositoryVendorItem, _logger );
		}

		[Fact]
		public async void Test_CreateAsync() {
			GetVendorItemDTO? result = null;

			try {
				result = await _serviceVendorItem.CreateAsync( 2, new AddVendorItemDTO {
					Brand = "Test Brand",
					Capacity = 1,
					VendorId = 1,
					Comments = "Test Comments",
					Price = 100,
					WarrantyDuration = "12",
					ProductCode = "1",
					ProductName = "Test ProductName",
				} );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllAsync() {
			IEnumerable<GetVendorItemDTO>? result = null;

			try {
				result = await _serviceVendorItem.GetAllAsync( 2, 1, 10 );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}


		[Fact]
		public async void Test_GetByIdAsync() {
			GetVendorItemDTO? result = null;

			try {
				result = await _serviceVendorItem.GetByIdAsync( 1 );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_UpdateAsync() {
			GetVendorItemDTO? result = null;

			try {
				result = await _serviceVendorItem.UpdateAsync( 2, 1, new AddVendorItemDTO {
					Brand = "Test Brand",
					Capacity = 1,
					VendorId = 1,
					Comments = "Test Comments",
					Price = 100,
					WarrantyDuration = "Test",
					ProductCode = "23",
					ProductName = "Test ProductName",
				} );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}


	}


}
