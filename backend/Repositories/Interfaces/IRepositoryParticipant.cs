using backend.Models.Domains;

namespace backend.Repositories.Interfaces {
	public interface IRepositoryParticipant {
		Task<IEnumerable<Participant>> GetAllByCompanyAsync( Int32 companyId );
		Task<IEnumerable<Participant>> GetAllNonEmlployeesByCompanyWithLimitAsync( Int32 companyId, Int32 offset, Int32 count );
		Task<IEnumerable<Participant>> GetAllEmlployeesByCompanyWithLimitAsync( Int32 companyId, Int32 offset, Int32 count );
		Task<Participant> GetByIdAsync( Int32 id );
		Task<Participant> CreateAsync( Participant participant );
		Task<Participant> UpdateAsync( Participant participant );
		Task<Boolean> DeleteAsync( Int32 id );
	}
}
