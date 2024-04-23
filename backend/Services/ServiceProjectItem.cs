using backend.Models.Domains;
using backend.Models.DTO.ProjectItem;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using FluentValidation;
using System.Numerics;

namespace backend.Services
{
    public class ServiceProjectItem : IServiceProjectItem
    {
        IRepositoryParticipant _repositoryParticipant;
        IRepositorySystemUser _repositorySystemUser;
        IRepositoryProject _repositoryProject;
        IRepositoryProjectItem _repositoryProjectItem;
        ILogger<ServiceCustomer> _logger;
        IValidator<AddProjectItemDTO> _validator;

        public ServiceProjectItem(IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryProject repositoryProject, IRepositoryProjectItem repositoryProjectItem, ILogger<ServiceCustomer> logger, IValidator<AddProjectItemDTO> validator )
        {
            _repositoryParticipant = repositoryParticipant;
            _repositorySystemUser = repositorySystemUser;
            _repositoryProject = repositoryProject;
            _repositoryProjectItem = repositoryProjectItem;
            _logger = logger;
            _validator = validator;
        }
        public async Task<GetProjectItemDTO> CreateAsync(int userId, AddProjectItemDTO projectItem)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            //validation
            var validateResult = await _validator.ValidateAsync(projectItem);
            if (!validateResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validateResult.Errors);
            }

            Project? project = null;

            try
            {
                project = await _repositoryProject.GetByIdAsync(projectItem.ProjectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            // Check if the project exists and if it belongs to the same company
            if (project == null || project.CompanyId != companyId)
            {
                throw new Exception("Project Item not found");
            }


            ProjectItem projectItemToCreate = new ProjectItem
            {
                Id = null,
                ProjectId=projectItem.ProjectId,
                SerialNo = projectItem.SerialNo,
                WarrantyDuration = projectItem.WarrantyDuration,
                ModuleNo = projectItem.ModuleNo,
                VendorItemId = projectItem.VendorItemId,
                Comments = projectItem.Comments,
                LastUpdatedBy = userId,
                LastUpdatedDateTime = null

                
                




            };

            ProjectItem? projectItemCreated = null;

            try
            {
                projectItemCreated = await _repositoryProjectItem.CreateAsync(projectItemToCreate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectItemCreated == null)
            {
                throw new Exception("No project Item was created");
            }

            return new GetProjectItemDTO
            {

                Id = projectItemCreated.Id,
                ProjectId = projectItemCreated.ProjectId,
                SerialNo = projectItemCreated.SerialNo,
                WarrantyDuration = projectItemCreated.WarrantyDuration,
                ModuleNo = projectItemCreated.ModuleNo,
                VendorItemId = projectItemCreated.VendorItemId,
                Comments = projectItemCreated.Comments,
                LastUpdatedBy = projectItemCreated.LastUpdatedBy,
                LastUpdatedDateTime = projectItemCreated.LastUpdatedDateTime


              
            };
        }

        public async Task<bool> DeleteAsync(int userId, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetProjectItemDTO>> GetAllAsync(int userId, int page, int pageSize)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            IEnumerable<ProjectItem>? projectItems = null;

            try
            {
                projectItems = await _repositoryProjectItem.GetAllByProjectWithLimitAsync(companyId, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectItems == null)
            {
                throw new Exception("No project Item found");
            }

            List<GetProjectItemDTO> projectItemDTO = new List<GetProjectItemDTO>();

            foreach (ProjectItem projectItem in projectItems)
            {
                projectItemDTO.Add(new GetProjectItemDTO
                {



                    

                    Id = projectItem.Id,
                    ProjectId = projectItem.ProjectId,
                    SerialNo = projectItem.SerialNo,
                    WarrantyDuration = projectItem.WarrantyDuration,
                    ModuleNo = projectItem.ModuleNo,
                    VendorItemId = projectItem.VendorItemId,
                    Comments = projectItem.Comments,
                    LastUpdatedBy = projectItem.LastUpdatedBy,
                    LastUpdatedDateTime = projectItem.LastUpdatedDateTime








                });
            }

            return projectItemDTO;
        }

        public async Task<IEnumerable<GetProjectItemDTO>> GetAllByProjectAsync(int userId, int projectId, int page, int pageSize)
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


            IEnumerable<ProjectItem>? projectItems = null;

            try
            {
                projectItems = await _repositoryProjectItem.GetAllByProjectWithLimitAsync(projectId, (page - 1) * pageSize, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectItems == null)
            {
                throw new Exception("No project Item found");
            }

            List<GetProjectItemDTO> projectItemDTO = new List<GetProjectItemDTO>();

            foreach (ProjectItem projectItem in projectItems)
            {
               projectItemDTO.Add(new GetProjectItemDTO
                {

                    Id = projectItem.Id,
                    ProjectId = projectItem.ProjectId,
                    SerialNo = projectItem.SerialNo,
                    WarrantyDuration = projectItem.WarrantyDuration,
                    ModuleNo = projectItem.ModuleNo,
                    VendorItemId = projectItem.VendorItemId,
                    Comments = projectItem.Comments,
                    LastUpdatedBy = projectItem.LastUpdatedBy,
                    LastUpdatedDateTime = projectItem.LastUpdatedDateTime

                });
            }

            return projectItemDTO;
        }

        public async Task<GetProjectItemDTO> GetByIdAsync(int id)
        {

            ProjectItem? projectItem = null;

            try
            {
                projectItem = await _repositoryProjectItem.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectItem == null)
            {
                throw new Exception("No project item found");
            }

            return new GetProjectItemDTO
            {




                Id = projectItem.Id,
                ProjectId = projectItem.ProjectId,
                SerialNo = projectItem.SerialNo,
                WarrantyDuration = projectItem.WarrantyDuration,
                ModuleNo = projectItem.ModuleNo,
                VendorItemId = projectItem.VendorItemId,
                Comments = projectItem.Comments,
                LastUpdatedBy = projectItem.LastUpdatedBy,
                LastUpdatedDateTime = projectItem.LastUpdatedDateTime

            };
        }

        public async Task<GetProjectItemDTO> UpdateAsync(int userId, int id, AddProjectItemDTO projectItem)
        {

            //validation
            var validateResult = await _validator.ValidateAsync(projectItem);
            if (!validateResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validateResult.Errors);
            }

            ProjectItem projectItemToUpdate = new ProjectItem
            {


               

                Id = id,
                ProjectId = projectItem.ProjectId,
                SerialNo = projectItem.SerialNo,
                WarrantyDuration = projectItem.WarrantyDuration,
                ModuleNo = projectItem.ModuleNo,
                VendorItemId = projectItem.VendorItemId,
                Comments = projectItem.Comments,
                LastUpdatedBy = userId,
                LastUpdatedDateTime = null






            };

            ProjectItem? projectItemUpdated = null;

            try
            {
                projectItemUpdated = await _repositoryProjectItem.UpdateAsync(projectItemToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectItemUpdated == null)
            {
                throw new Exception("No project item was updated");
            }

            return new GetProjectItemDTO
            {

                Id = projectItemUpdated.Id,
                ProjectId = projectItemUpdated.ProjectId,
                SerialNo = projectItemUpdated.SerialNo,
                WarrantyDuration = projectItemUpdated.WarrantyDuration,
                ModuleNo = projectItemUpdated.ModuleNo,
                VendorItemId = projectItemUpdated.VendorItemId,
                Comments = projectItemUpdated.Comments,
                LastUpdatedBy = projectItemUpdated.LastUpdatedBy,
                LastUpdatedDateTime = projectItemUpdated.LastUpdatedDateTime


            };
        }
    }
}
