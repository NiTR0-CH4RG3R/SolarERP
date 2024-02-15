using backend.Models.Domains;

namespace backend.Repositories.Interfaces {
	public interface IRepositoryTask {
		Task<IEnumerable<Models.Domains.Task>> GetAllByCompanyWithLimitAsync( Int32 companyId, Int32 offset, Int32 count );
		Task<IEnumerable<Models.Domains.Task>> GetAllByAssignedToWithLimitAsync( Int32 userId, Int32 offset, Int32 count );
		Task<IEnumerable<Models.Domains.Task>> GetAllByUrgencyLevelWithLimitAsync( UrgencyLevel urgencyLevel, Int32 offset, Int32 count );
		Task<Models.Domains.Task> GetByIdAsync( Int32 id );
		Task<Models.Domains.Task> CreateAsync( Models.Domains.Task task );
		Task<Models.Domains.Task> UpdateAsync( Models.Domains.Task task );
		Task<Boolean> DeleteAsync( Int32 id );
	}
}
