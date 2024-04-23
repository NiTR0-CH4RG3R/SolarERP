using static backend.Models.Domains.ProjectService;

namespace backend.Repositories.Interfaces
{
    public interface IRepositoryProjectService
    {
        Task<IEnumerable<Models.Domains.ProjectService>> GetAllByProjectIdWithLimitAsync(Int32 projectId, Int32 offset, Int32 count);
        Task<Models.Domains.ProjectService> GetByIdAsync(Int32 id);
        Task<IEnumerable<Models.Domains.ProjectService>> GetAllProjectsByPendingStatusWithLimitAsync(Int32 companyId, ProjectServiceStatus status, Int32 offset, Int32 count);
        Task<Models.Domains.ProjectService> CreateAsync(Models.Domains.ProjectService projectService);
        Task<Models.Domains.ProjectService> UpdateAsync(Models.Domains.ProjectService projectService);
        Task<Boolean> DeleteAsync(Int32 id);
    }
}
