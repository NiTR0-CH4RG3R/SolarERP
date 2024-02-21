using backend.Models.Domains;
using backend.Models.DTO.ProjectResource;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;

namespace backend.Services
{
    public class ServiceProjectResource : IServiceProjectResource
    {
        IRepositoryParticipant _repositoryParticipant;
        IRepositorySystemUser _repositorySystemUser;
        IRepositoryProject _repositoryProject;
        IRepositoryProjectResource _repositoryProjectResource;
        ILogger<ServiceCustomer> _logger;

        public ServiceProjectResource(IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryProject repositoryProject, IRepositoryProjectResource repositoryProjectResource, ILogger<ServiceCustomer> logger)
        {
            _repositoryParticipant = repositoryParticipant;
            _repositorySystemUser = repositorySystemUser;
            _repositoryProject = repositoryProject;
            _repositoryProjectResource = repositoryProjectResource;
            _logger = logger;
        }
        public async Task<GetProjectResourceDTO> CreateAsync(int userId, AddProjectResourceDTO projectResource)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            Project? project = null;

            try
            {
                project = await _repositoryProject.GetByIdAsync(projectResource.ProjectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            // Check if the vendor exists and if it belongs to the same company
            if (project == null || project.CompanyId != companyId)
            {
                throw new Exception("Project not found");
            }

            ProjectResource projectResourceToCreate = new ProjectResource
            {
                Id = null,
                ProjectId=projectResource.ProjectId,
                Category=projectResource.Category,
                URL=projectResource.URL,
                Comments=projectResource.Comments,
                LastUpdatedBy = userId,
                LastUpdatedDateTime = null,




    };

            ProjectResource? projectResourceCreated = null;

            try
            {
                projectResourceCreated = await _repositoryProjectResource.CreateAsync( projectResourceToCreate );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectResourceCreated == null)
            {
                throw new Exception("No project resource was created");
            }

            return new GetProjectResourceDTO
            {
                

                Id = projectResourceCreated.Id,
                ProjectId = projectResourceCreated.ProjectId,
                Category = projectResourceCreated.Category,
                URL = projectResourceCreated.URL,
                Comments = projectResourceCreated.Comments,
                LastUpdatedBy = projectResourceCreated.LastUpdatedBy,
                LastUpdatedDateTime = projectResourceCreated.LastUpdatedDateTime
            };
        }

        public async Task<bool> DeleteAsync(int userId, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetProjectResourceDTO>> GetAllAsync(int userId, int page, int pageSize)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            IEnumerable<ProjectResource>? projectResources = null;

            try
            {
                projectResources = await _repositoryProjectResource.GetAllByProjectWithLimitAsync(companyId, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectResources == null)
            {
                throw new Exception("No project resource found");
            }

            List<GetProjectResourceDTO> projectResourcesDTO = new List<GetProjectResourceDTO>();

            foreach (ProjectResource projectResource in projectResources)
            {
                projectResourcesDTO.Add(new GetProjectResourceDTO
                {
                

                    Id = projectResource.Id,
                    ProjectId = projectResource.ProjectId,
                    Category = projectResource.Category,
                    URL = projectResource.URL,
                    Comments = projectResource.Comments,
                    LastUpdatedBy = projectResource.LastUpdatedBy,
                    LastUpdatedDateTime = projectResource.LastUpdatedDateTime,
                });
            }

            return projectResourcesDTO;
        }

        public async Task<IEnumerable<GetProjectResourceDTO>> GetAllByProjectAsync(int userId, int projectId, int page, int pageSize)
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


            IEnumerable<ProjectResource>? projectResources = null;

            try
            {
                projectResources = await _repositoryProjectResource.GetAllByProjectWithLimitAsync(projectId, (page - 1) * pageSize, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectResources == null)
            {
                throw new Exception("No project resource found");
            }

            List<GetProjectResourceDTO> projectResourcesDTO = new List<GetProjectResourceDTO>();

            foreach (ProjectResource projectResource in projectResources)
            {
                projectResourcesDTO.Add(new GetProjectResourceDTO
                {
                  

                    Id = projectResource.Id,
                    ProjectId = projectResource.ProjectId,
                    Category = projectResource.Category,
                    URL = projectResource.URL,
                    Comments = projectResource.Comments,
                    LastUpdatedBy = projectResource.LastUpdatedBy,
                    LastUpdatedDateTime = projectResource.LastUpdatedDateTime,
                });
            }

            return projectResourcesDTO;
        }

        public async Task<GetProjectResourceDTO> GetByIdAsync(int id)
        {

            ProjectResource? projectResource = null;

            try
            {
              projectResource = await _repositoryProjectResource.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectResource == null)
            {
                throw new Exception("No project resource found");
            }

            return new GetProjectResourceDTO
            {
               



                Id =projectResource.Id ,
                ProjectId = projectResource.ProjectId,
                Category = projectResource.Category,
                URL = projectResource.URL,
                Comments = projectResource.Comments,
                LastUpdatedBy = projectResource.LastUpdatedBy,
                LastUpdatedDateTime = projectResource.LastUpdatedDateTime,
            };
        }

        public async Task<GetProjectResourceDTO> UpdateAsync(int userId, int id, AddProjectResourceDTO projectResource)
        {
            ProjectResource projectResourceToUpdate = new ProjectResource
            {
                Id = id,
                ProjectId = projectResource.ProjectId,
                Category = projectResource.Category,
                URL = projectResource.URL,
                Comments = projectResource.Comments,
                LastUpdatedBy = userId,
                LastUpdatedDateTime = null


            };

            ProjectResource? projectResourceUpdated = null;

            try
            {
                projectResourceUpdated = await _repositoryProjectResource.UpdateAsync(projectResourceToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectResourceUpdated == null)
            {
                throw new Exception("No project resource was updated");
            }

            return new GetProjectResourceDTO
            {
                


                Id = projectResourceUpdated.Id,
                ProjectId = projectResourceUpdated.ProjectId,
                Category = projectResourceUpdated.Category,
                URL = projectResourceUpdated.URL,
                Comments = projectResourceUpdated.Comments,
                LastUpdatedBy = projectResourceUpdated.LastUpdatedBy,
                LastUpdatedDateTime = projectResourceUpdated.LastUpdatedDateTime
            };
        }
    }
}
