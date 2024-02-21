using backend.Models.DTO.ProjectResource;

namespace backend.Services.Interfaces
{
    public interface IServiceProjectResource
    {
        Task<IEnumerable<GetProjectResourceDTO>> GetAllAsync(Int32 userId, Int32 page, Int32 pageSize);
        Task<IEnumerable<GetProjectResourceDTO>> GetAllByProjectAsync(Int32 userId, Int32 projectId, Int32 page, Int32 pageSize);
        Task<GetProjectResourceDTO> GetByIdAsync(Int32 id);
        Task<GetProjectResourceDTO> CreateAsync(Int32 userId, AddProjectResourceDTO projectResource);
        Task<GetProjectResourceDTO> UpdateAsync(Int32 userId, Int32 id, AddProjectResourceDTO projectResource);
        Task<Boolean> DeleteAsync(Int32 userId, Int32 id);
    }
}
