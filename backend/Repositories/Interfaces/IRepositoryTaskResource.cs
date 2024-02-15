using backend.Models.Domains;

namespace backend.Repositories.Interfaces
{
    public interface IRepositoryTaskResource
    {
        Task<IEnumerable<TaskResource>> GetAllByTaskIdAsync(Int32 taskId, Int32 offset, Int32 count);
        Task<IEnumerable<TaskResource>> GetAllByTaskIdAndURLAsync(Int32 taskId, String url, Int32 offset, Int32 count);
        Task<TaskResource> CreateAsync(TaskResource taskresource);
        Task<Boolean> DeleteByTaskIdAndURLAsync(Int32 taskId, String URL);
    }
}
