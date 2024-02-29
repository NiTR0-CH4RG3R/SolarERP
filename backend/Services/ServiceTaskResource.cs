using backend.Models.Domains;
using backend.Models.DTO.Task;
using backend.Models.DTO.TaskReource;
using backend.Models.DTO.TaskStatus;
using backend.Models.DTO.VendorItem;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.Design;
using System.Security.Policy;
using System.Threading.Tasks;

namespace backend.Services
{
    public class ServiceTaskResource : IServiceTaskResource
    {
        IRepositoryParticipant _repositoryParticipant;
        IRepositorySystemUser _repositorySystemUser;
        IRepositoryTask _repositoryTask;
        IRepositoryTaskResource _repositoryTaskResource;
        ILogger<ServiceCustomer> _logger;

        public ServiceTaskResource(IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryTask repositoryTask, IRepositoryTaskResource repositoryTaskResource, ILogger<ServiceCustomer> logger)
        {
            _repositoryParticipant = repositoryParticipant;
            _repositorySystemUser = repositorySystemUser;
            _repositoryTask = repositoryTask;
            _repositoryTaskResource = repositoryTaskResource;
            _logger = logger;
        }
        public async Task<GetTaskResourceDTO> CreateAsync(int userId, AddTaskResourceDTO taskResource)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);
            Models.Domains.Task? task = null;

            try
            {
                task = await _repositoryTask.GetByIdAsync(taskResource.TaskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            // Check if the task exists and if it belongs to the same company
            if (task == null || task.CompanyId != companyId)
            {
                throw new Exception("Task not found");
            }

            TaskResource taskResourceToCreate = new TaskResource
            {
                TaskId = taskResource.TaskId,
                URL = taskResource.URL,
                Category = taskResource.Category,
                Comments = taskResource.Comments,
                LastUpdatedBy = userId,
                LastUpdatedDateTime = null
            };

            TaskResource? taskResourceCreated = null;

            try
            {
                taskResourceCreated = await _repositoryTaskResource.CreateAsync(taskResourceToCreate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (taskResourceCreated == null)
            {
                throw new Exception("No task resource was created");
            }

            return new GetTaskResourceDTO
            {
                TaskId = taskResourceCreated.TaskId,
                URL = taskResourceCreated.URL,
                Category = taskResourceCreated.Category,
                Comments = taskResourceCreated.Comments,
                LastUpdatedBy = taskResourceCreated.LastUpdatedBy,
                LastUpdatedDateTime = taskResourceCreated.LastUpdatedDateTime
            };
        }

        public Task<bool> DeleteByTaskIdAndURLAsync(int userId, int taskId, string URL)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetTaskResourceDTO>> GetAllByTaskIdAsync(int userId, int taskId)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            Models.Domains.Task? task = null;

            try
            {
                task = await _repositoryTask.GetByIdAsync(taskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (task == null || task.CompanyId != companyId)
            {
                throw new Exception("No data found");
            }

            IEnumerable<Models.Domains.TaskResource>? taskResources = null;

            try
            {
                taskResources = await _repositoryTaskResource.GetAllByTaskIdAsync(taskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (taskResources == null)
            {
                throw new Exception("No data found");
            }

            List<GetTaskResourceDTO> taskResourceDTOs = new List<GetTaskResourceDTO>();

            foreach (Models.Domains.TaskResource taskResource in taskResources)
            {
                GetTaskResourceDTO taskResourceDTO = new GetTaskResourceDTO
                {
                    TaskId = taskResource.TaskId,
                    URL = taskResource.URL,
                    Category = taskResource.Category,
                    Comments = taskResource.Comments,
                    LastUpdatedBy = taskResource.LastUpdatedBy,
                    LastUpdatedDateTime = taskResource.LastUpdatedDateTime
                };

                taskResourceDTOs.Add(taskResourceDTO);
            }

            return taskResourceDTOs;
        }

        public async Task<IEnumerable<GetTaskResourceDTO>> GetByTaskIdAndURLAsync(int userId, int taskId, string url)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            Models.Domains.Task? task = null;

            try
            {
                task = await _repositoryTask.GetByIdAsync(taskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (task == null || task.CompanyId != companyId)
            {
                throw new Exception("No data found");
            }

            IEnumerable<Models.Domains.TaskResource>? taskResources = null;


            try
            {
                taskResources = await _repositoryTaskResource.GetByTaskIdAndURLAsync(taskId, url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (taskResources == null)
            {
                throw new Exception("No data found");
            }

            List<GetTaskResourceDTO> taskResourceDTOs = new List<GetTaskResourceDTO>();

            foreach (Models.Domains.TaskResource taskResource in taskResources)
            {
                GetTaskResourceDTO taskResourceDTO = new GetTaskResourceDTO
                {
                    TaskId = taskResource.TaskId,
                    URL = taskResource.URL,
                    Category = taskResource.Category,
                    Comments = taskResource.Comments,
                    LastUpdatedBy = taskResource.LastUpdatedBy,
                    LastUpdatedDateTime = taskResource.LastUpdatedDateTime
                };

                taskResourceDTOs.Add(taskResourceDTO);
            }

            return taskResourceDTOs;
        }
    }
}
