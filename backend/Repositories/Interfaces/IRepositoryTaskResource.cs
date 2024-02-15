using backend.Models.Domains;

namespace backend.Repositories.Interfaces
{
    public interface IRepositoryTaskResource
    {
        Task<IEnumerable<TaskResource>> GetAllByTaskIdAsync(Int32 taskId);
        Task<IEnumerable<TaskResource>> GetByTaskIdAndURLAsync(Int32 taskId, String url);
        Task<TaskResource> CreateAsync(TaskResource taskresource);
        Task<Boolean> DeleteByTaskIdAndURLAsync(Int32 taskId, String URL);
    }
}
