using backend.Models.Domains;
using backend.Models.DTO.Customer;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.test.Services {
	public class ServiceCustomerTest {
		private readonly IDbConnection _connection;
		private readonly IRepositoryParticipant _repositoryParticipant;
		private readonly IRepositorySystemUser _repositorySystemUser;
		private readonly ILogger<ServiceCustomer> _logger;
		private readonly IServiceCustomer _serviceCustomer;

		public ServiceCustomerTest() {
			var serviceProvider = new ServiceCollection()
									.AddLogging()
									.BuildServiceProvider();

			var factory = serviceProvider.GetService<ILoggerFactory>();

			_logger = factory.CreateLogger<ServiceCustomer>();

			_connection = new MySqlConnection("Server=localhost;Database=new_erp;Uid=root;Pwd=BlackDragon321@b;Port=3306");
			_connection.Open();

			_repositoryParticipant = new RepositoryParticipant(_connection);
			_repositorySystemUser = new RepositorySystemUser(_connection);

			//_serviceCustomer = new ServiceCustomer(_repositoryParticipant, _repositorySystemUser, _logger);
		}

		[Fact]
		public async void Test_CreateAsync() {
			GetCustomerDTO? result = null;

			try {
				result = await _serviceCustomer.CreateAsync(
					2,
					new AddCustomerDTO {
						FirstName = "Test First Name",
						LastName = "Test Last Name",
						Email = "",
						Phone01 = "",
						Phone02 = "",
						Address = "Test Address",
						Category = ParticipantCategory.Customer.ToString(),
						Profession = "Test Profession",
						Comments = "Test Comments",
					}
				);
			}
			catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			Assert.NotNull(result);
		}

		[Fact]
		public async void Test_GetAllByCompanyAsync() {
			IEnumerable<GetCustomerDTO>? result = null;

			try {
				result = await _serviceCustomer.GetAllByPagesAsync( 2, 1, 10 );
			}
			catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			Assert.NotNull(result);
		}

		[Fact]
		public async void Test_GetByIdAsync() {
			GetCustomerDTO? result = null;

			try {
				result = await _serviceCustomer.GetByIdAsync( 1 );
			}
			catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			Assert.NotNull(result);
		}

		[Fact]
		public async void Test_UpdateAsync() {
			GetCustomerDTO? result = null;

			try {
				result = await _serviceCustomer.UpdateAsync( 1, 2, new AddCustomerDTO {
					FirstName = "Test First Name",
					LastName = "Test Last Name",
					Email = "",
					Phone01 = "",
					Phone02 = "",
					Address = "Test Address",
					Category = ParticipantCategory.Customer.ToString(),
					Profession = "Test Profession",
					Comments = "Test Comments",
				} );
			}
			catch (Exception ex) {
				Assert.Fail(ex.Message);
			}

			Assert.NotNull(result);
		}


	}
}
