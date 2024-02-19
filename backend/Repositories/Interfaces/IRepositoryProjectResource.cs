using backend.Models.Domains;

namespace backend.Repositories.Interfaces
{
    public interface IRepositoryProjectResource
    {
        Task<ProjectResource> GetByIdAsync(Int32 id);
        Task<ProjectResource> CreateAsync (ProjectResource projectResource);
        Task<ProjectResource> UpdateAsync (ProjectResource projectResource);
    }
}
