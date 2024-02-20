using backend.Models.Domains;

namespace backend.Repositories.Interfaces
{
    public interface IRepositoryProjectResource
    {
        Task<IEnumerable<ProjectResource>> GetAllByProjectWithLimitAsync(int projectId, int offset, int count);
        Task<ProjectResource> GetByIdAsync(Int32 id);
        Task<ProjectResource> CreateAsync (ProjectResource projectResource);
        Task<ProjectResource> UpdateAsync (ProjectResource projectResource);
    }
}
