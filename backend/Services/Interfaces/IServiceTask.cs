using backend.Models.Domains;
using backend.Models.DTO.Task;

namespace backend.Services.Interfaces {
	public interface IServiceTask {
		Task<IEnumerable<GetTaskDTO>> GetAllAsync( Int32 userId, Int32 page, Int32 pageSize );
		Task<IEnumerable<GetTaskDTO>> GetAllByAssignedTo( Int32 userId, Int32 page, Int32 pageSize );
		Task<IEnumerable<GetTaskDTO>> GetAllByUrgencyLevel( Int32 userId, TaskUrgencyLevel urgencyLevel, Int32 page, Int32 pageSize );
		Task<IEnumerable<GetTaskDTO>> GetAllByCategory( Int32 userId, TaskCategories category, Int32 page, Int32 pageSize );
		Task<IEnumerable<GetTaskDTO>> GetAllByCategoryAndUrgencyLevel(Int32 userId, TaskCategories category, TaskUrgencyLevel urgencyLevel, Int32 page, Int32 pageSize);
		Task<GetTaskDTO> GetByIdAsync( Int32 id );
		Task<GetTaskDTO> CreateAsync( Int32 userId, AddTaskDTO task );
		Task<GetTaskDTO> UpdateAsync( Int32 userId, Int32 id, AddTaskDTO task );
		Task<Boolean> DeleteAsync( Int32 userId, Int32 id );
	}
}
