using backend.Models.Domains;

namespace backend.Repositories.Interfaces
{
    public interface IRepositoryTaskStatus
    {
        Task<IEnumerable<Models.Domains.TaskStatus>> GetAllByIdAsync(Int32 id, Int32 offset, Int32 count);
        Task<IEnumerable<Models.Domains.TaskStatus>> GetAllByTaskIdWithLimitAsync(Int32 taskId, Int32 offset, Int32 count);
        Task<Models.Domains.TaskStatus> CreateAsync(Models.Domains.TaskStatus taskstatus);
        Task<Models.Domains.TaskStatus> UpdateAsync(Models.Domains.TaskStatus taskstatus);
        Task<Boolean> DeleteById(Int32 taskId);
    }
}
