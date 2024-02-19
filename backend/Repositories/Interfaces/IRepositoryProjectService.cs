namespace backend.Repositories.Interfaces
{
    public interface IRepositoryProjectService
    {
        Task<Models.Domains.ProjectService> GetByIdAsync(Int32 id);
        Task<Models.Domains.ProjectService> CreateAsync(Models.Domains.ProjectService projectService);
        Task<Models.Domains.ProjectService> UpdateAsync(Models.Domains.ProjectService projectService);
        Task<Boolean> DeleteAsync(Int32 id);
    }
}
