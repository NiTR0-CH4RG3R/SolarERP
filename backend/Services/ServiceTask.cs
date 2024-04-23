using backend.Models.Domains;
using backend.Models.DTO.Task;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;

namespace backend.Services {
	public class ServiceTask : IServiceTask {

		IRepositoryParticipant _repositoryParticipant;
		IRepositorySystemUser _repositorySystemUser;
		IRepositoryTask _repositoryTask;
		ILogger<ServiceCustomer> _logger;
        IValidator<AddTaskDTO> _validator;

        public ServiceTask( IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryTask repositoryTask, ILogger<ServiceCustomer> logger, IValidator<AddTaskDTO> validator ) {
			_repositoryParticipant = repositoryParticipant;
			_repositorySystemUser = repositorySystemUser;
			_repositoryTask = repositoryTask;
			_logger = logger;
            _validator = validator;
        }

		public async Task<GetTaskDTO> CreateAsync( int userId, AddTaskDTO task ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			//Validation
			var validationResult = await _validator.ValidateAsync(task);
            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            Models.Domains.Task taskToCreate = new Models.Domains.Task {
				Id = null,
				CompanyId = companyId,
				Description = task.Description,
				Comments = task.Comments,
				LastUpdatedBy = userId,
				LastUpdatedDateTime = null,
				AssignedTo = task.AssignedTo,
				Category = task.Category,
				CallBackNumber = task.CallBackNumber,
				ProjectId = task.ProjectId,
				RequestedBy = task.RequestedBy,
				UrgencyLevel = task.UrgencyLevel
			};

			Models.Domains.Task? taskCreated = null;

			try {
				taskCreated = await _repositoryTask.CreateAsync( taskToCreate );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( taskCreated == null ) {
				throw new Exception( "No task was created" );
			}

			return new GetTaskDTO {
				Id = taskCreated.Id,
				Description = taskCreated.Description,
				Comments = taskCreated.Comments,
				LastUpdatedBy = taskCreated.LastUpdatedBy,
				LastUpdatedDateTime = taskCreated.LastUpdatedDateTime,
				AssignedTo = taskCreated.AssignedTo,
				Category = taskCreated.Category,
				CallBackNumber = taskCreated.CallBackNumber,
				ProjectId = taskCreated.ProjectId,
				RequestedBy = taskCreated.RequestedBy,
				UrgencyLevel = taskCreated.UrgencyLevel
			};

		}

		public async Task<bool> DeleteAsync( int userId, int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<GetTaskDTO>> GetAllAsync( int userId, int page, int pageSize ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			IEnumerable<Models.Domains.Task>? tasks = null;

			try {
				tasks = await _repositoryTask.GetAllByCompanyWithLimitAsync( companyId, (page - 1) * pageSize, pageSize );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( tasks == null ) {
				throw new Exception( "No tasks were found" );
			}

			List<GetTaskDTO> tasksDTO = new List<GetTaskDTO>();

			foreach ( Models.Domains.Task task in tasks ) {
				tasksDTO.Add( new GetTaskDTO {
					Id = task.Id,
					Description = task.Description,
					Comments = task.Comments,
					LastUpdatedBy = task.LastUpdatedBy,
					LastUpdatedDateTime = task.LastUpdatedDateTime,
					AssignedTo = task.AssignedTo,
					Category = task.Category,
					CallBackNumber = task.CallBackNumber,
					ProjectId = task.ProjectId,
					RequestedBy = task.RequestedBy,
					UrgencyLevel = task.UrgencyLevel,
					Status = task.Status,
				} );
			}

			return tasksDTO;
		}

		public async Task<IEnumerable<GetTaskDTO>> GetAllByAssignedTo( int userId, int page, int pageSize ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId);

			IEnumerable<Models.Domains.Task>? tasks = null;

			try {
				tasks = await _repositoryTask.GetAllByAssignedToWithLimitAsync(userId, ( page - 1 ) * pageSize, pageSize); 
			}
			catch (Exception ex) {
				_logger.LogError(ex, ex.Message);
			}

			if (tasks == null) {
				throw new Exception("No tasks were found");
			}

			List<GetTaskDTO> tasksDTO = new List<GetTaskDTO>();

			foreach (Models.Domains.Task task in tasks) {
				tasksDTO.Add( new GetTaskDTO {
					Id = task.Id,
					Description = task.Description,
					Comments = task.Comments,
					LastUpdatedBy = task.LastUpdatedBy,
					LastUpdatedDateTime = task.LastUpdatedDateTime,
					AssignedTo = task.AssignedTo,
					Category = task.Category,
					CallBackNumber = task.CallBackNumber,
					ProjectId = task.ProjectId,
					RequestedBy = task.RequestedBy,
					UrgencyLevel = task.UrgencyLevel
				} );
			}

			return tasksDTO;
		}

		public async Task<IEnumerable<GetTaskDTO>> GetAllByUrgencyLevel( int userId, TaskUrgencyLevel urgencyLevel, int page, int pageSize ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId);

			IEnumerable<Models.Domains.Task>? tasks = null;

			try {
				tasks = await _repositoryTask.GetAllByCompanyAndUrgencyLevelWithLimitAsync( companyId, urgencyLevel, (page - 1) * pageSize, pageSize );
			}
			catch (Exception ex) {
				_logger.LogError(ex, ex.Message);
			}

			if (tasks == null) {
				throw new Exception("No tasks were found");
			}

			List<GetTaskDTO> tasksDTO = new List<GetTaskDTO>();

			foreach ( Models.Domains.Task task in tasks ) {
				tasksDTO.Add( new GetTaskDTO {
					Id = task.Id,
					Description = task.Description,
					Comments = task.Comments,
					LastUpdatedBy = task.LastUpdatedBy,
					LastUpdatedDateTime = task.LastUpdatedDateTime,
					AssignedTo = task.AssignedTo,
					Category = task.Category,
					CallBackNumber = task.CallBackNumber,
					ProjectId = task.ProjectId,
					RequestedBy = task.RequestedBy,
					UrgencyLevel = task.UrgencyLevel,
                    Status = task.Status
                } );
			}

			return tasksDTO;

		}

		public async Task<GetTaskDTO> GetByIdAsync( int id ) {
			Models.Domains.Task? task = null;

			try {
				task = await _repositoryTask.GetByIdAsync( id );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( task == null ) {
				throw new Exception( "No task was found" );
			}

			return new GetTaskDTO {
				Id = task.Id,
				Description = task.Description,
				Comments = task.Comments,
				LastUpdatedBy = task.LastUpdatedBy,
				LastUpdatedDateTime = task.LastUpdatedDateTime,
				AssignedTo = task.AssignedTo,
				Category = task.Category,
				CallBackNumber = task.CallBackNumber,
				ProjectId = task.ProjectId,
				RequestedBy = task.RequestedBy,
				UrgencyLevel = task.UrgencyLevel,
				Status = task.Status
			};
		}

        public async Task<IEnumerable<GetTaskDTO>> GetAllByCategory(int userId, TaskCategories category , int page, int pageSize)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            IEnumerable<Models.Domains.Task>? tasks = null;

            try
            {
                tasks = await _repositoryTask.GetAllByCompanyAndCategoryWithLimitAsync(companyId, category, (page - 1) * pageSize, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (tasks == null)
            {
                throw new Exception("No tasks were found");
            }

            List<GetTaskDTO> tasksDTO = new List<GetTaskDTO>();

            foreach (Models.Domains.Task task in tasks)
            {
                tasksDTO.Add(new GetTaskDTO
                {
                    Id = task.Id,
                    Description = task.Description,
                    Comments = task.Comments,
                    LastUpdatedBy = task.LastUpdatedBy,
                    LastUpdatedDateTime = task.LastUpdatedDateTime,
                    AssignedTo = task.AssignedTo,
                    Category = task.Category,
                    CallBackNumber = task.CallBackNumber,
                    ProjectId = task.ProjectId,
                    RequestedBy = task.RequestedBy,
                    UrgencyLevel = task.UrgencyLevel,
                    Status = task.Status
                });
            }

            return tasksDTO;
        }

        public async Task<GetTaskDTO> UpdateAsync( int userId, int id, AddTaskDTO task ) {
			
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			//Validation
            var validateResult = await _validator.ValidateAsync(task);
            if (!validateResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validateResult.Errors);
            }

            Models.Domains.Task taskToUpdate = new Models.Domains.Task {
				Id = id,
				CompanyId = companyId,
				Description = task.Description,
				Comments = task.Comments,
				LastUpdatedBy = userId,
				LastUpdatedDateTime = null,
				AssignedTo = task.AssignedTo,
				Category = task.Category,
				CallBackNumber = task.CallBackNumber,
				ProjectId = task.ProjectId,
				RequestedBy = task.RequestedBy,
				UrgencyLevel = task.UrgencyLevel
			};

            Models.Domains.Task? taskUpdated = null;

			try {
				taskUpdated = await _repositoryTask.UpdateAsync( taskToUpdate );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( taskUpdated == null ) {
				throw new Exception( "No task was updated" );
			}

			return new GetTaskDTO {
				Id = taskUpdated.Id,
				Description = taskUpdated.Description,
				Comments = taskUpdated.Comments,
				LastUpdatedBy = taskUpdated.LastUpdatedBy,
				LastUpdatedDateTime = taskUpdated.LastUpdatedDateTime,
				AssignedTo = taskUpdated.AssignedTo,
				Category = taskUpdated.Category,
				CallBackNumber = taskUpdated.CallBackNumber,
				ProjectId = taskUpdated.ProjectId,
				RequestedBy = taskUpdated.RequestedBy,
				UrgencyLevel = taskUpdated.UrgencyLevel
			};
		}

        public async Task<IEnumerable<GetTaskDTO>> GetAllByCategoryAndUrgencyLevel(int userId, TaskCategories category, TaskUrgencyLevel urgencyLevel, int page, int pageSize)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            IEnumerable<Models.Domains.Task>? tasks = null;

            try
            {
                tasks = await _repositoryTask.GetAllByCompanyWithCategoryAndUrgencyLevelLimitAsync(companyId, category, urgencyLevel, (page - 1) * pageSize, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (tasks == null)
            {
                throw new Exception("No tasks were found");
            }

            List<GetTaskDTO> tasksDTO = new List<GetTaskDTO>();

            foreach (Models.Domains.Task task in tasks)
            {
                tasksDTO.Add(new GetTaskDTO
                {
                    Id = task.Id,
                    Description = task.Description,
                    Comments = task.Comments,
                    LastUpdatedBy = task.LastUpdatedBy,
                    LastUpdatedDateTime = task.LastUpdatedDateTime,
                    AssignedTo = task.AssignedTo,
                    Category = task.Category,
                    CallBackNumber = task.CallBackNumber,
                    ProjectId = task.ProjectId,
                    RequestedBy = task.RequestedBy,
                    UrgencyLevel = task.UrgencyLevel,
                    Status = task.Status
                });
            }

            return tasksDTO;
        }

    }
}
