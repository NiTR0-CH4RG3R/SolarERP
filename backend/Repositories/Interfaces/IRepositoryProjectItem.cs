using backend.Models.Domains;

namespace backend.Repositories.Interfaces
{
    public interface IRepositoryProjectItem
    {
        Task<IEnumerable<ProjectItem>> GetAllByProjectWithLimitAsync(Int32 projectId, Int32 offset, Int32 count);
        Task<ProjectItem> GetByIdAsync(Int32 id);
        Task<ProjectItem> CreateAsync(ProjectItem projectItem);
        Task<ProjectItem> UpdateAsync(ProjectItem projectItem);
    }
}
