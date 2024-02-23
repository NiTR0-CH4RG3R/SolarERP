using backend.Models.DTO.ProjectItem;

namespace backend.Services.Interfaces
{
    public interface IServiceProjectItem
    {
        Task<IEnumerable<GetProjectItemDTO>> GetAllAsync(Int32 userId, Int32 page, Int32 pageSize);
        Task<IEnumerable<GetProjectItemDTO>> GetAllByProjectAsync(Int32 userId, Int32 projectId, Int32 page, Int32 pageSize);
        Task<GetProjectItemDTO> GetByIdAsync(Int32 id);
        Task<GetProjectItemDTO> CreateAsync(Int32 userId, AddProjectItemDTO projectItem);
        Task<GetProjectItemDTO> UpdateAsync(Int32 userId, Int32 id, AddProjectItemDTO projectItem);
        Task<Boolean> DeleteAsync(Int32 userId, Int32 id);
    }
}
