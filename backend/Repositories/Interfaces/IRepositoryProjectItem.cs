using backend.Models.Domains;

namespace backend.Repositories.Interfaces
{
    public interface IRepositoryProjectItem
    {
        Task<ProjectItem> GetByIdAsync(Int32 id);
        Task<ProjectItem> CreateAsync(ProjectItem projectItem);
        Task<ProjectItem> UpdateAsync(ProjectItem projectItem);
    }
}
