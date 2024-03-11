<<<<<<< HEAD
﻿using backend.Models.Domains;
using backend.Models.DTO.ProjectService;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace backend.Services
{
    public class ServiceProjectService : IServiceProjectService
    {
        IRepositoryParticipant _repositoryParticipant;
        IRepositorySystemUser _repositorySystemUser;
        IRepositoryProject _repositoryProject;
        IRepositoryProjectService _repositoryProjectService;
        ILogger<ServiceCustomer> _logger;

        public ServiceProjectService(IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryProject repositoryProject, IRepositoryProjectService repositoryProjectService, ILogger<ServiceCustomer> logger)
        {
            _repositoryParticipant = repositoryParticipant;
            _repositorySystemUser = repositorySystemUser;
            _repositoryProject = repositoryProject;
            _repositoryProjectService = repositoryProjectService;
            _logger = logger;
        }
        public async Task<GetProjectServiceDTO> CreateAsync(int userId, AddProjectServiceDTO projectService)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            Project? project = null;

            try
            {
                project = await _repositoryProject.GetByIdAsync(projectService.ProjectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            // Check if the project exists and if it belongs to the same company
            if (project == null || project.CompanyId != companyId)
            {
                throw new Exception("Project not found");
            }

            ProjectService projectServiceToCreate = new ProjectService
            {
                Id = null,
                ProjectId = projectService.ProjectId,
                PlannedDate = projectService.PlannedDate,
                Status = projectService.Status,
                ConductedBy = projectService.ConductedBy,
                ConductedDate = projectService.ConductedDate,
                Priority = projectService.Priority,
                Description = projectService.Description,
                ServiceReportURL = projectService.ServiceReportURL,
                ServiceLevel = projectService.ServiceLevel,
                LastUpdatedBy = userId,
                LastUpdatedDateTime = DateTime.UtcNow,

            };

            ProjectService? projectServiceCreated = null;

            try
            {
                projectServiceCreated = await _repositoryProjectService.CreateAsync(projectServiceToCreate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectServiceCreated == null)
            {
                throw new Exception("No project service was created");
            }

            return new GetProjectServiceDTO
            {

                Id = projectServiceCreated.Id,
                ProjectId = projectServiceCreated.ProjectId,
                PlannedDate = projectServiceCreated.PlannedDate,
                Status = projectServiceCreated.Status,
                ConductedBy = projectServiceCreated.ConductedBy,
                ConductedDate = projectServiceCreated.ConductedDate,
                Priority = projectServiceCreated.Priority,
                Description = projectServiceCreated.Description,
                ServiceReportURL = projectServiceCreated.ServiceReportURL,
                ServiceLevel = projectServiceCreated.ServiceLevel,
                LastUpdatedBy = projectServiceCreated.LastUpdatedBy,
                LastUpdatedDateTime = projectServiceCreated.LastUpdatedDateTime
            };
        }

        public async Task<bool> DeleteAsync(int userId, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetProjectServiceDTO>> GetAllAsync(int userId, int page, int pageSize)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            IEnumerable<ProjectService>? projectServices = null;

            try
            {
                projectServices = await _repositoryProjectService.GetAllByProjectIdWithLimitAsync(companyId, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectServices == null)
            {
                throw new Exception("No project service found");
            }

            List<GetProjectServiceDTO> projectServicesDTO = new List<GetProjectServiceDTO>();

            foreach (ProjectService projectService in projectServices)
            {
                projectServicesDTO.Add(new GetProjectServiceDTO
                {


                    Id = projectService.Id,
                    ProjectId = projectService.ProjectId,
                    PlannedDate = projectService.PlannedDate,
                    Status = projectService.Status,
                    ConductedBy = projectService.ConductedBy,
                    ConductedDate = projectService.ConductedDate,
                    Priority = projectService.Priority,
                    Description = projectService.Description,
                    ServiceReportURL = projectService.ServiceReportURL,
                    ServiceLevel = projectService.ServiceLevel,
                    LastUpdatedBy = projectService.LastUpdatedBy,
                    LastUpdatedDateTime = projectService.LastUpdatedDateTime,
                });
            }

            return projectServicesDTO;
        }

        public async Task<IEnumerable<GetProjectServiceDTO>> GetAllByProjectAsync(int userId, int projectId, int page, int pageSize)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            Project? project = null;

            try
            {
                project = await _repositoryProject.GetByIdAsync(projectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }



            if (project == null || project.CompanyId != companyId)
            {
                throw new Exception("project not found");
            }


            IEnumerable<ProjectService>? projectServices = null;

            try
            {
                projectServices = await _repositoryProjectService.GetAllByProjectIdWithLimitAsync(projectId, (page - 1) * pageSize, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectServices == null)
            {
                throw new Exception("No project service found");
            }

            List<GetProjectServiceDTO> projectServicesDTO = new List<GetProjectServiceDTO>();

            foreach (ProjectService projectService in projectServices)
            {
                projectServicesDTO.Add(new GetProjectServiceDTO
                {


                    Id = projectService.Id,
                    ProjectId = projectService.ProjectId,
                    PlannedDate = projectService.PlannedDate,
                    Status = projectService.Status,
                    ConductedBy = projectService.ConductedBy,
                    ConductedDate = projectService.ConductedDate,
                    Priority = projectService.Priority,
                    Description = projectService.Description,
                    ServiceReportURL = projectService.ServiceReportURL,
                    ServiceLevel = projectService.ServiceLevel,
                    LastUpdatedBy = projectService.LastUpdatedBy,
                    LastUpdatedDateTime = projectService.LastUpdatedDateTime,
                });
            }

            return projectServicesDTO;
        }


        public async Task<GetProjectServiceDTO> GetByIdAsync(int id)
        {

            ProjectService? projectService = null;

            try
            {
                projectService = await _repositoryProjectService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectService == null)
            {
                throw new Exception("No project service found");
            }

            return new GetProjectServiceDTO
            {

                Id = projectService.Id,
                ProjectId = projectService.ProjectId,
                PlannedDate = projectService.PlannedDate,
                Status = projectService.Status,
                ConductedBy = projectService.ConductedBy,
                ConductedDate = projectService.ConductedDate,
                Priority = projectService.Priority,
                Description = projectService.Description,
                ServiceReportURL = projectService.ServiceReportURL,
                ServiceLevel = projectService.ServiceLevel,
                LastUpdatedBy = projectService.LastUpdatedBy,
                LastUpdatedDateTime = projectService.LastUpdatedDateTime,
            };
        }

        public async Task<GetProjectServiceDTO> UpdateAsync(int userId, int id, AddProjectServiceDTO projectService)
        {
            ProjectService projectServiceToUpdate = new ProjectService { Id = null, ProjectId = projectService.ProjectId, PlannedDate = projectService.PlannedDate, Status = projectService.Status, ConductedBy = projectService.ConductedBy, ConductedDate = projectService.ConductedDate, Priority = projectService.Priority, Description = projectService.Description, ServiceReportURL = projectService.ServiceReportURL, ServiceLevel = projectService.ServiceLevel, LastUpdatedBy = userId, LastUpdatedDateTime = DateTime.UtcNow, };

            ProjectService? projectServiceUpdated = null;

            try
            {
                projectServiceUpdated = await _repositoryProjectService.UpdateAsync(projectServiceToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectServiceUpdated == null)
            {
                throw new Exception("No project service was updated");
            }

            return new GetProjectServiceDTO
            {

                Id = projectServiceUpdated.Id,
                ProjectId = projectServiceUpdated.ProjectId,
                PlannedDate = projectServiceUpdated.PlannedDate,
                Status = projectServiceUpdated.Status,
                ConductedBy = projectServiceUpdated.ConductedBy,
                ConductedDate = projectServiceUpdated.ConductedDate,
                Priority = projectServiceUpdated.Priority,
                Description = projectServiceUpdated.Description,
                ServiceReportURL = projectServiceUpdated.ServiceReportURL,
                ServiceLevel = projectServiceUpdated.ServiceLevel,
                LastUpdatedBy = projectServiceUpdated.LastUpdatedBy,
                LastUpdatedDateTime = projectServiceUpdated.LastUpdatedDateTime
            };
        }

        Task<IEnumerable<GetProjectServiceDTO>> IServiceProjectService.GetAllByProjectIdAsync(int userId, int projectId, int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
=======
﻿using AutoMapper;
using backend.Models.Domains;
using backend.Models.DTO.ProjectService;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mysqlx.Crud;

namespace backend.Services {
	public class ServiceProjectService( IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryProject repositoryProject, IRepositoryProjectService repositoryProjectService, IMapper mapper, ILogger<ServiceCustomer> logger ) : IServiceProjectService {


		public async Task<GetProjectServiceDTO> CreateAsync( int userId, AddProjectServiceDTO projectService ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( logger, repositoryParticipant, repositorySystemUser, userId );

			ProjectService projectServiceToBeCreated = mapper.Map<ProjectService>( projectService );

			Project? project = null;

			try {
				project = await repositoryProject.GetByIdAsync( projectService.ProjectId );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( project == null || project.CompanyId != companyId ) {
				throw new Exception( "Project not found" );
			}

			ProjectService? projectServiceCreated = null;

			try {
				projectServiceCreated = await repositoryProjectService.CreateAsync( projectServiceToBeCreated );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( projectServiceCreated == null ) {
				throw new Exception( "Project Service not created" );
			}

			return mapper.Map<GetProjectServiceDTO>( projectServiceCreated );
		}

		public async Task<bool> DeleteAsync( int userId, int id ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( logger, repositoryParticipant, repositorySystemUser, userId );

			throw new NotImplementedException();
		}

		public async Task<IEnumerable<GetProjectServiceDTO>> GetAllAsync( int userId, int page, int pageSize ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( logger, repositoryParticipant, repositorySystemUser, userId );

			IEnumerable<ProjectService>? projectServices = null;

			try {
				projectServices = await repositoryProjectService.GetAllByProjectIdWithLimitAsync( companyId, ( page - 1 ) * pageSize , pageSize );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( projectServices == null ) {
				throw new Exception( "Project Service not found" );
			}

			return mapper.Map<IEnumerable<GetProjectServiceDTO>>( projectServices );

		}

		public async Task<IEnumerable<GetProjectServiceDTO>> GetAllByProjectAsync( int userId, int projectId, int page, int pageSize ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( logger, repositoryParticipant, repositorySystemUser, userId );

			Project? project = null;

			try {
				project = await repositoryProject.GetByIdAsync( projectId );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( project == null || project.CompanyId != companyId ) {
				throw new Exception( "Project not found" );
			}

			IEnumerable<ProjectService>? projectServices = null;

			try {
				projectServices = await repositoryProjectService.GetAllByProjectIdWithLimitAsync( projectId, ( page - 1 ) * pageSize, pageSize );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( projectServices == null ) {
				throw new Exception( "Project Service not found" );
			}

			return mapper.Map<IEnumerable<GetProjectServiceDTO>>( projectServices );
		}

		public async Task<GetProjectServiceDTO> GetByIdAsync( int id ) {
			ProjectService? projectService = null;


			try {
				projectService = await repositoryProjectService.GetByIdAsync( id );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( projectService == null ) {
				throw new Exception( "Project Service not found" );
			}

			return mapper.Map<GetProjectServiceDTO>( projectService );
		}

		public async Task<GetProjectServiceDTO> UpdateAsync( int userId, int id, AddProjectServiceDTO projectService ) {

			Int32 companyId = await ServiceUtilities.GetCompanyId( logger, repositoryParticipant, repositorySystemUser, userId );

			ProjectService projectServiceToBeUpdated = mapper.Map<ProjectService>(projectService);

			projectServiceToBeUpdated.Id = id;


			ProjectService? updatedService = null;


			try {
				updatedService = await repositoryProjectService.UpdateAsync( projectServiceToBeUpdated );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( updatedService == null ) {
				throw new Exception( "Project Service not found" );
			}

			return mapper.Map<GetProjectServiceDTO>( updatedService );
		}
	}
>>>>>>> Renamed TaskResource
}