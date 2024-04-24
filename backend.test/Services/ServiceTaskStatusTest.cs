using backend.Models.Domains;
using backend.Models.DTO.TaskResource;
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
using backend.Models.DTO.TaskStatus;
using backend.Models.DTO.VendorItem;
using backend.Models.DTO.Project;

namespace backend.test.Services
{
    public class ServiceTaskStatusTest
    {
        private readonly IDbConnection _connection;
        private readonly IRepositoryParticipant _repositoryParticipant;
        private readonly IRepositorySystemUser _repositorySystemUser;
        private readonly IRepositoryTask _repositoryTask;
        private readonly IRepositoryTaskStatus _repositoryTaskStatus;
        private readonly ILogger<ServiceCustomer> _logger;
        private readonly IServiceTaskStatus _serviceTaskStatus;

        public ServiceTaskStatusTest()
        {
            var serviceProvider = new ServiceCollection()
                                    .AddLogging()
                                    .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            _logger = factory.CreateLogger<ServiceCustomer>();

            _connection = new MySqlConnection( "Server=db-mysql-blr1-08473-do-user-14661818-0.c.db.ondigitalocean.com;Database=new_erp;Uid=doadmin;Pwd=AVNS_VXDCh37l8j_R6GxtxAI;Port=25060" );
            _connection.Open();

            _repositoryParticipant = new RepositoryParticipant(_connection);
            _repositorySystemUser = new RepositorySystemUser(_connection);
            _repositoryTask = new RepositoryTask(_connection);
            _repositoryTaskStatus = new RepositoryTaskStatus(_connection);

            _serviceTaskStatus = new ServiceTaskStatus(_repositoryParticipant, _repositorySystemUser, _repositoryTask, _repositoryTaskStatus, _logger);
        }

        [Fact]
        public async void Test_CreateAsync()
        {
            GetTaskStatusDTO? result = null;

            try
            {
                result = await _serviceTaskStatus.CreateAsync(2, new AddTaskStatusDTO
                {
                    TaskId = 3,
                    Status = TaskStatusCategory.Active.ToString(),
                    Comments = "This is a task status category for test"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_UpdateAsync()
        {
            GetTaskStatusDTO? result = null;

            try
            {
                result = await _serviceTaskStatus.UpdateAsync(3, 2, new AddTaskStatusDTO
                {

                    TaskId = 3,
                    Status = TaskStatusCategory.Active.ToString(),
                    Comments = "Comment updated ",
                    
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            Assert.NotNull(result);
        }

        [Fact]
        public async void Test_GetByIdAsync()
        {
            // Act
            GetTaskStatusDTO? result = null;
            try
            {
                result = await _serviceTaskStatus.GetByIdAsync(2);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            // Assert
            Assert.NotNull(result);
        }
        
    }
}
