using backend.Models.DTO.TaskReource;

namespace backend.Services.Interfaces {
	public interface IServiceTaskResource {
		Task<IEnumerable<GetTaskResourceDTO>> GetAllByTaskIdAsync( Int32 userId, Int32 taskId);
		Task<GetTaskResourceDTO> GetByTaskIdAndURLAsync(Int32 taskId, String url);
		Task<GetTaskResourceDTO> CreateAsync( Int32 userId, AddTaskResourceDTO taskResource);
		Task<Boolean> DeleteByTaskIdAndURLAsync( Int32 userId, Int32 taskId, String URL);
	}
}
