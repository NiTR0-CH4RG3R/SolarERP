using backend.Models.Domains;

namespace backend.Repositories.Interfaces {
	public interface IRepositoryProjectCommisionReport {
		Task<IEnumerable<ProjectCommisionReport>> GetAllByProjectAsync( Int32 projectId );
		Task<IEnumerable<ProjectCommisionReport>> GetAllByProjectWithLimitAsync( Int32 projectId, Int32 offset, Int32 count );
		Task<ProjectCommisionReport> GetByIdAsync( Int32 id );
		Task<ProjectCommisionReport> CreateAsync( ProjectCommisionReport projectCommisionReport );
		Task<ProjectCommisionReport> UpdateAsync( ProjectCommisionReport projectCommisionReport );
		Task<Boolean> DeleteAsync( Int32 id );
	}
}
