using backend.Models.DTO.TaskStatus;

namespace backend.Services.Interfaces {
	public interface IServiceTaskStatus {
		Task<GetTaskStatusDTO> CreateAsync(Int32 userId, AddTaskStatusDTO taskStatus);
		Task<GetTaskStatusDTO> GetByIdAsync(Int32 id);
		Task<List<GetTaskStatusDTO>> GetAllByTaskAsync( Int32 userId, Int32 taskId);
		Task<GetTaskStatusDTO> UpdateAsync(Int32 userId, Int32 id, AddTaskStatusDTO taskStatus);
		Task<Boolean> DeleteAsync(Int32 userId, Int32 id);

	}
}
