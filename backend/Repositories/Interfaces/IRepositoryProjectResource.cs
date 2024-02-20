using backend.Models.Domains;

namespace backend.Repositories.Interfaces
{
    public interface IRepositoryProjectResource
    {
        Task<IEnumerable<ProjectResource>> GetAllByProjectWithLimitAsync(Int32 projectId, Int32 offset, Int32 count);
        Task<ProjectResource> GetByIdAsync(Int32 id);
        Task<ProjectResource> CreateAsync (ProjectResource projectResource);
        Task<ProjectResource> UpdateAsync (ProjectResource projectResource);
    }
}
