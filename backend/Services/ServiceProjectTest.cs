using backend.Models.Domains;
using backend.Models.DTO.ProjectTest;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;

namespace backend.Services
{
    public class ServiceProjectTest : IServiceProjectTest
    {
        IRepositoryParticipant _repositoryParticipant;
        IRepositorySystemUser _repositorySystemUser;
        IRepositoryProject _repositoryProject;
        IRepositoryProjectTest _repositoryProjectTest;
        ILogger<ServiceCustomer> _logger;

        public ServiceProjectTest(IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryProject repositoryProject, IRepositoryProjectTest repositoryProjectTest, ILogger<ServiceCustomer> logger)
        {
            _repositoryParticipant = repositoryParticipant;
            _repositorySystemUser = repositorySystemUser;
            _repositoryProject = repositoryProject;
            _repositoryProjectTest = repositoryProjectTest;
            _logger = logger;
        }
        public async Task<GetProjectTestDTO> CreateAsync(int userId, AddProjectTestDTO projectTest)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            Project? project = null;

            try
            {
                project = await _repositoryProject.GetByIdAsync(projectTest.ProjectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            // Check if the project exists and if it belongs to the same company
            if (project == null || project.CompanyId != companyId)
            {
                throw new Exception("Project Test not found");
            }

            
            ProjectTest projectTestToCreate = new ProjectTest
            {
                Id = null,
                Name = projectTest.Name,
                ProjectId=projectTest.ProjectId,
                ConductedDate = projectTest.ConductedDate,
                ConductedBy = projectTest.ConductedBy,
                Passed = projectTest.Passed,
                Comments = projectTest.Comments,
                LastUpdatedBy = userId,
                LastUpdatedDateTime = DateTime.UtcNow, 




            };

            ProjectTest? projectTestCreated = null;

            try
            {
                projectTestCreated = await _repositoryProjectTest.CreateAsync(projectTestToCreate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectTestCreated == null)
            {
                throw new Exception("No project Test was created");
            }

            return new GetProjectTestDTO
            {




                Id = projectTestCreated.Id,
                ProjectId=projectTest.ProjectId,
                Name = projectTestCreated.Name,
                ConductedDate = projectTestCreated.ConductedDate,
                ConductedBy = projectTestCreated.ConductedBy,
                Passed = projectTestCreated.Passed,
                Comments = projectTestCreated.Comments,
                LastUpdatedBy = projectTestCreated.LastUpdatedBy,
                LastUpdatedDateTime = projectTestCreated.LastUpdatedDateTime,
            };
        }

        public async Task<bool> DeleteAsync(int userId, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetProjectTestDTO>> GetAllAsync(int userId, int page, int pageSize)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

            IEnumerable<ProjectTest>? projectTests = null;

            try
            {
                projectTests = await _repositoryProjectTest.GetAllByProjectIdWithLimitAsync(companyId, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectTests == null)
            {
                throw new Exception("No project Test found");
            }

            List<GetProjectTestDTO> projectTestDTO = new List<GetProjectTestDTO>();

            foreach (ProjectTest projectTest in projectTests)
            {
                projectTestDTO.Add(new GetProjectTestDTO
                {



                    Id = projectTest.Id,
                    ProjectId=projectTest.ProjectId,
                    Name = projectTest.Name,
                    ConductedDate = projectTest.ConductedDate,
                    ConductedBy = projectTest.ConductedBy,
                    Passed = projectTest.Passed,
                    Comments = projectTest.Comments,
                    LastUpdatedBy = projectTest.LastUpdatedBy,
                    LastUpdatedDateTime = projectTest.LastUpdatedDateTime,








                });
            }

            return projectTestDTO;
        }

        public async Task<IEnumerable<GetProjectTestDTO>> GetAllByProjectAsync(int userId, int projectId, int page, int pageSize)
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


            IEnumerable<ProjectTest>? projectTests = null;

            try
            {
                projectTests = await _repositoryProjectTest.GetAllByProjectIdWithLimitAsync(projectId, (page - 1) * pageSize, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectTests == null)
            {
                throw new Exception("No project Test found");
            }

            List<GetProjectTestDTO> projectTestDTO = new List<GetProjectTestDTO>();

            foreach (ProjectTest projectTest in projectTests)
            {
                projectTestDTO.Add(new GetProjectTestDTO
                {



                    Id = projectTest.Id,
                    ProjectId = projectTest.ProjectId,
                    Name = projectTest.Name,
                    ConductedDate = projectTest.ConductedDate,
                    ConductedBy = projectTest.ConductedBy,
                    Passed = projectTest.Passed,
                    Comments = projectTest.Comments,
                    LastUpdatedBy = projectTest.LastUpdatedBy,
                    LastUpdatedDateTime = projectTest.LastUpdatedDateTime,
                });
            }

            return projectTestDTO;
        }

        public async Task<GetProjectTestDTO> GetByIdAsync(int id)
        {

            ProjectTest? projectTest = null;

            try
            {
                projectTest = await _repositoryProjectTest.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectTest == null)
            {
                throw new Exception("No project test found");
            }

            return new GetProjectTestDTO
            {






                Id = projectTest.Id,
                ProjectId = projectTest.ProjectId,
                Name = projectTest.Name,
                ConductedDate = projectTest.ConductedDate,
                ConductedBy = projectTest.ConductedBy,
                Passed = projectTest.Passed,
                Comments = projectTest.Comments,
                LastUpdatedBy = projectTest.LastUpdatedBy,
                LastUpdatedDateTime = projectTest.LastUpdatedDateTime,
            };
        }

        public async Task<GetProjectTestDTO> UpdateAsync(int userId, int id, AddProjectTestDTO projectTest)
        {
            ProjectTest projectTestToUpdate = new ProjectTest
            {


                Id = id,
                ProjectId = projectTest.ProjectId,
                Name = projectTest.Name,
                ConductedDate = projectTest.ConductedDate,
                ConductedBy = projectTest.ConductedBy,
                Passed = projectTest.Passed,
                Comments = projectTest.Comments,
                LastUpdatedBy = userId,
                LastUpdatedDateTime = DateTime.UtcNow,





            };

            ProjectTest? projectTestUpdated = null;

            try
            {
                projectTestUpdated = await _repositoryProjectTest.UpdateAsync(projectTestToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            if (projectTestUpdated == null)
            {
                throw new Exception("No project test was updated");
            }

            return new GetProjectTestDTO
            {



         


                 Id = projectTestUpdated.Id,
                ProjectId = projectTestUpdated.ProjectId,
                Name = projectTestUpdated.Name,
                ConductedDate = projectTestUpdated.ConductedDate,
                ConductedBy = projectTestUpdated.ConductedBy,
                Passed = projectTestUpdated.Passed,
                Comments = projectTestUpdated.Comments,
                LastUpdatedBy = projectTestUpdated.LastUpdatedBy,
                LastUpdatedDateTime = projectTestUpdated.LastUpdatedDateTime,

            };
        }
    }
}
