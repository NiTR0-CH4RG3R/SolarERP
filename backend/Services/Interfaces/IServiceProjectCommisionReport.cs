using backend.Models.DTO.ProjectCommisionReport;

namespace backend.Services.Interfaces {
	public interface IServiceProjectCommisionReport {
		Task<IEnumerable<GetProjectCommisionReportDTO>> GetAllByProjectAsync( Int32 userId, Int32 projectId, Int32 page, Int32 pageSize );
		Task<GetProjectCommisionReportDTO> GetByIdAsync( Int32 id );
		Task<GetProjectCommisionReportDTO> CreateAsync( Int32 userId, AddProjectCommisionReportDTO projectCommisionReport );
		Task<GetProjectCommisionReportDTO> UpdateAsync( Int32 userId, Int32 id, AddProjectCommisionReportDTO projectCommisionReport );
		Task<Boolean> DeleteAsync( Int32 userId, Int32 id );
	}
}
