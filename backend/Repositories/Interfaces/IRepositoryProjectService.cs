namespace backend.Repositories.Interfaces
{
    public interface IRepositoryProjectService
    {
        Task<Models.Domains.ProjectService> GetByIdAsync(Int32 id);
        Task<Models.Domains.ProjectService> CreateAsync(Models.Domains.ProjectService projectService);
    }
}
