using backend.Models.DTO.ProjectService;

namespace backend.Services.Interfaces
{
    public interface IServiceProjectService
    {
        Task<IEnumerable<GetProjectServiceDTO>> GetAllAsync(Int32 userId, Int32 page, Int32 pageSize);
        Task<IEnumerable<GetProjectServiceDTO>> GetAllByProjectIdAsync(Int32 userId, Int32 projectId, Int32 page, Int32 pageSize);
        Task<GetProjectServiceDTO> GetByIdAsync(Int32 id);
        Task<GetProjectServiceDTO> CreateAsync(Int32 userId, AddProjectServiceDTO projectService);
        Task<GetProjectServiceDTO> UpdateAsync(Int32 userId, Int32 id, AddProjectServiceDTO projectService);
        Task<Boolean> DeleteAsync(Int32 userId, Int32 id);
    }
}
