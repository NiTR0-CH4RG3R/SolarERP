using backend.Models.Domains;
using backend.Models.DTO.Customer;
using backend.Models.DTO.Project;
using backend.Models.DTO.TaskReource;
using backend.Models.DTO.TaskStatus;
using backend.Models.DTO.VendorItem;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace backend.Services
{
    public class ServiceTaskStatus : IServiceTaskStatus
    {
        IRepositoryParticipant _repositoryParticipant;
        IRepositorySystemUser _repositorySystemUser;
        IRepositoryTask _repositoryTask;
        IRepositoryTaskStatus _repositoryTaskStatus;
        ILogger<ServiceCustomer> _logger;

        public ServiceTaskStatus(IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryTask repositoryTask, IRepositoryTaskStatus repositoryTaskStatus, ILogger<ServiceCustomer> logger)
        {
            _repositoryParticipant = repositoryParticipant;
            _repositorySystemUser = repositorySystemUser;
            _repositoryTask = repositoryTask;
            _repositoryTaskStatus = repositoryTaskStatus;
            _logger = logger;
        }
        public async Task<GetTaskStatusDTO> CreateAsync(int userId, AddTaskStatusDTO taskStatus)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);
            Models.Domains.Task? task = null;

            try
            {
                task = await _repositoryTask.GetByIdAsync(taskStatus.TaskId);
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

            Models.Domains.TaskStatus taskStatusToCreate = new Models.Domains.TaskStatus
            {
                Id = null,
                TaskId = taskStatus.TaskId,
                Status = taskStatus.Status,
                Comments = taskStatus.Comments,
                LastUpdatedBy = userId,
                LastUpdatedDateTime = null
            };
            Models.Domains.TaskStatus? taskStatusCreated = null;

            try
            {
                taskStatusCreated = await _repositoryTaskStatus.CreateAsync(taskStatusToCreate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (taskStatusCreated == null)
            {
                throw new Exception("No task status was created");
            }

            return new GetTaskStatusDTO
            {
                Id = taskStatusCreated.Id,
                TaskId = taskStatusCreated.TaskId,
                Status = taskStatusCreated.Status,
                Comments = taskStatusCreated.Comments,
                LastUpdatedBy = taskStatusCreated.LastUpdatedBy,
                LastUpdatedDateTime = taskStatusCreated.LastUpdatedDateTime
            };
        }

        public Task<bool> DeleteAsync(int userId, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetTaskStatusDTO>> GetAllByTaskAsync(int userId, int taskId, int page, int pageSize)
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

            IEnumerable<Models.Domains.TaskStatus>? taskStatuses = null;

            try
            {
                taskStatuses = await _repositoryTaskStatus.GetAllByTaskIdWithLimitAsync(taskId, (page - 1) * pageSize, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (taskStatuses == null)
            {
                throw new Exception("No data found");
            }

            List<GetTaskStatusDTO> taskStatusDTOs = new List<GetTaskStatusDTO>();

            foreach (Models.Domains.TaskStatus taskStatus in taskStatuses)
            {
                GetTaskStatusDTO taskStatusDTO = new GetTaskStatusDTO
                {
                    Id = taskStatus.Id,
                    TaskId = taskStatus.TaskId,
                    Status = taskStatus.Status,
                    Comments = taskStatus.Comments,
                    LastUpdatedBy = taskStatus.LastUpdatedBy,
                    LastUpdatedDateTime = taskStatus.LastUpdatedDateTime
                };

                taskStatusDTOs.Add(taskStatusDTO);
            }

            return taskStatusDTOs;

        }

        public async Task<GetTaskStatusDTO> GetByIdAsync(int id)
        {
            Models.Domains.TaskStatus? taskStatus = null;

            try
            {
                taskStatus = (Models.Domains.TaskStatus?)await _repositoryTaskStatus.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
            }

            if (taskStatus == null)
            {
                throw new Exception("No task status found");
            }

            return new GetTaskStatusDTO
            {
                Id = taskStatus.Id,
                TaskId = taskStatus.TaskId,
                Status = taskStatus.Status,
                Comments = taskStatus.Comments,
                LastUpdatedBy = taskStatus.LastUpdatedBy,
                LastUpdatedDateTime = taskStatus.LastUpdatedDateTime
            };


        }

        public async Task<GetTaskStatusDTO> UpdateAsync(int userId, int id, AddTaskStatusDTO taskStatus)
        {
            Models.Domains.TaskStatus taskStatusToUpdate = new Models.Domains.TaskStatus
            {
                Id = id,
                TaskId = taskStatus.TaskId,
                Status = taskStatus.Status,
                Comments = taskStatus.Comments,
                LastUpdatedBy = userId,
                LastUpdatedDateTime = null
            };

            Models.Domains.TaskStatus? taskStatusUpdated = null;

            try
            {
                taskStatusUpdated = await _repositoryTaskStatus.UpdateAsync(taskStatusToUpdate);
             
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (taskStatusUpdated == null)
            {
                throw new Exception("No task status was updated");
            }

            return new GetTaskStatusDTO
            {
                Id = taskStatusUpdated.Id,
                TaskId = taskStatusUpdated.TaskId,
                Status = taskStatusUpdated.Status,
                Comments = taskStatusUpdated.Comments,
                LastUpdatedBy = taskStatusUpdated.LastUpdatedBy,
                LastUpdatedDateTime = taskStatusUpdated.LastUpdatedDateTime
            };
        }
    }
}
