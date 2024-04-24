using backend.Models.DTO.VendorItem;
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
using System.Security.Policy;
using backend.Models.DTO.TaskResource;

namespace backend.test.Services
{
    public class ServiceTaskResourceTest
    {
        private readonly IDbConnection _connection;
        private readonly IRepositoryParticipant _repositoryParticipant;
        private readonly IRepositorySystemUser _repositorySystemUser;
        private readonly IRepositoryTask _repositoryTask;
        private readonly IRepositoryTaskResource _repositoryTaskResource;
        private readonly ILogger<ServiceCustomer> _logger;
        private readonly IServiceTaskResource _serviceTaskResource;

        public ServiceTaskResourceTest()
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
            _repositoryTaskResource = new RepositoryTaskResource(_connection);

            _serviceTaskResource = new ServiceTaskResource(_repositoryParticipant, _repositorySystemUser, _repositoryTask, _repositoryTaskResource, _logger);
        }

        [Fact]
        public async void Test_CreateAsync()
        {
            GetTaskResourceDTO? result = null;

            try
            {
                result = await _serviceTaskResource.CreateAsync(2, new AddTaskResourceDTO
                {
                    TaskId = 2, 
                    URL = "taskresource4.jpg",
                    Category = TaskResourceCategory.Document.ToString(),
                    Comments = "This is a service task resource test comment"
                }); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            Assert.NotNull(result);
        }
    }
}
