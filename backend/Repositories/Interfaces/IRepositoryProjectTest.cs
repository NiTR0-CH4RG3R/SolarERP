namespace backend.Repositories.Interfaces
{
    public interface IRepositoryProjectTest
    {
        Task<Models.Domains.ProjectTest> GetByIdAsync(Int32 id);
    }
}
