using backend.Models.DTO.ProjectTest;

namespace backend.Services.Interfaces
{
    public interface IServiceProjectTest
    {
        Task<IEnumerable<GetProjectTestDTO>> GetAllAsync(Int32 userId, Int32 page, Int32 pageSize);
        Task<IEnumerable<GetProjectTestDTO>> GetAllByProjectAsync(Int32 userId, Int32 projectId, Int32 page, Int32 pageSize);
        Task<GetProjectTestDTO> GetByIdAsync(Int32 id);
        Task<GetProjectTestDTO> CreateAsync(Int32 userId, AddProjectTestDTO projectTest);
        Task<GetProjectTestDTO> UpdateAsync(Int32 userId, Int32 id, AddProjectTestDTO projectTest);
        Task<Boolean> DeleteAsync(Int32 userId, Int32 id);
    }
}
