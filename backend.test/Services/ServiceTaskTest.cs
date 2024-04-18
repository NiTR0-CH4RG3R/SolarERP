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
using backend.Models.DTO.Task;
using backend.Models.Domains;

namespace backend.test.Services {
	public class ServiceTaskTest {
		private readonly IDbConnection _connection;
		private readonly IRepositoryParticipant _repositoryParticipant;
		private readonly IRepositorySystemUser _repositorySystemUser;
		private readonly IRepositoryTask _repositoryTask;
		private readonly ILogger<ServiceCustomer> _logger;
		private readonly IServiceTask _serviceTask;

		public ServiceTaskTest() {
			var serviceProvider = new ServiceCollection()
									.AddLogging()
									.BuildServiceProvider();

			var factory = serviceProvider.GetService<ILoggerFactory>();

			_logger = factory.CreateLogger<ServiceCustomer>();

			_connection = new MySqlConnection( "Server=localhost;Database=new_erp;Uid=root;Pwd=;Port=3306" );
			_connection.Open();

			_repositoryParticipant = new RepositoryParticipant( _connection );
			_repositorySystemUser = new RepositorySystemUser( _connection );
			_repositoryTask = new RepositoryTask( _connection );

			_serviceTask = new ServiceTask( _repositoryParticipant, _repositorySystemUser, _repositoryTask, _logger );
		}

		[Fact]
		public async void Test_CreateAsync() {
			GetTaskDTO? result = null;

			try {
				result = await _serviceTask.CreateAsync( 2, new AddTaskDTO {
					Description = "Test Description",
					AssignedTo = 1,
					CallBackNumber = "123",
					Comments = "Test Comments",
					Category = TaskCategories.Inquiry.ToString(),
					ProjectId = null,
					RequestedBy = 1,
					UrgencyLevel = TaskUrgencyLevel.Low.ToString()
				} ) ;
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_UpdateAsync() {
			GetTaskDTO? result = null;

			try {
				result = await _serviceTask.UpdateAsync( 2, 3, new AddTaskDTO {
					Description = "Test Description",
					AssignedTo = 1,
					CallBackNumber = "123",
					Comments = "Test Comments",
					Category = TaskCategories.Inquiry.ToString(),
					ProjectId = null,
					RequestedBy = 1,
					UrgencyLevel = TaskUrgencyLevel.Low.ToString()
				} ) ;
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}


		[Fact]
		public async void Test_GetAllAsync() {
			IEnumerable<GetTaskDTO>? result = null;

			try {
				result = await _serviceTask.GetAllAsync( 2, 1, 10 );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetByIdAsync() {
			GetTaskDTO? result = null;

			try {
				result = await _serviceTask.GetByIdAsync( 2 );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			Assert.NotNull( result );
		}


		[Fact]

		public async void Test_GetAllByCategory()
		{
            IEnumerable<GetTaskDTO>? result = null;

            try
            {
                result = await _serviceTask.GetAllByCategory(3, TaskCategories.Inquiry, 1, 5);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            Assert.NotNull(result);
        }

		[Fact]
		public async void Test_GetAllByUrgencyLevel()
        {
            IEnumerable<GetTaskDTO>? result = null;

            try
            {
                result = await _serviceTask.GetAllByUrgencyLevel(3, TaskUrgencyLevel.Critical, 1, 5);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_GetAllByAssignedTo()
        {
            IEnumerable<GetTaskDTO>? result = null;

            try
            {
                result = await _serviceTask.GetAllByAssignedTo(3, 1, 5);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            Assert.NotNull(result);
        }

    }
}
