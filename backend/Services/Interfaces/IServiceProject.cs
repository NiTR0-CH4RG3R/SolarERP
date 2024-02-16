using backend.Models.DTO.Project;

namespace backend.Services.Interfaces {
	public interface IServiceProject {
		Task<IEnumerable<GetProjectDTO>> GetAllWithLimitAsync( Int32 userId, Int32 page, Int32 pageSize );
		Task<GetProjectDTO> GetByIdAsync( Int32 id );
		Task<GetProjectDTO> CreateAsync( Int32 userId, AddProjectDTO project );
		Task<GetProjectDTO> UpdateAsync( Int32 userId, Int32 id, AddProjectDTO project );
		Task<Boolean> DeleteAsync( Int32 userId, Int32 id );
	}
}
