namespace backend.Repositories.Interfaces
{
    public interface IRepositoryProjectTest
    {
        Task<IEnumerable<Models.Domains.ProjectTest>> GetAllByProjectIdWithLimitAsync(Int32 projectId, Int32 offset,  Int32 count);
        Task<Models.Domains.ProjectTest> GetByIdAsync(Int32 id);
        Task<Models.Domains.ProjectTest> CreateAsync(Models.Domains.ProjectTest projectTest);
        Task<Models.Domains.ProjectTest> UpdateAsync(Models.Domains.ProjectTest projectTest);
        Task<Boolean> DeleteAsync(Int32 id);
    }
}
