using backend.Models.DTO.Login;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services;
using backend.Services.Interfaces;
using backend.test.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.test.Services {
	public class ServiceAuthenticationTest {

		private readonly IServiceAuthentication _service;
		private readonly IConfiguration _configuration;
		private readonly IDbConnection _connection;
		private readonly IRepositorySystemUser _repositorySystemUser;

		public ServiceAuthenticationTest() {
			Dictionary<string, string> inMemorySettings = new Dictionary<string, string> {
				{"Jwt:Key", "YourSecretKeyForAuthenticationOfApplication"},
			};

			_configuration = new ConfigurationBuilder()
				.AddInMemoryCollection( inMemorySettings )
				.Build();

			_connection = new MySqlConnection( "Server=localhost;Database=new_erp;Uid=root;Pwd=BlackDragon321@b;Port=3306" );
			_connection.Open();

			_repositorySystemUser = new RepositorySystemUser( _connection );

			// create a dummy logger
			ILogger<ServiceAuthentication> logger = new LoggerFactory().CreateLogger<ServiceAuthentication>();

			_service = new ServiceAuthentication( _repositorySystemUser, _configuration, logger );
		}

		[Fact]
		public async void Test_AuthenticateAsync() {
			(string, string)? result = null;

			try {
				result = await _service.AuthenticateAsync( new LoginDTO { Username = "ADMIN2", Password = "ADMINPWD" } );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

	}
}
