using backend.Models.Domains;

namespace backend.Repositories.Interfaces {
	public interface IRepositoryVendor {
		Task<IEnumerable<Vendor>> GetAllByCompanyWithLimitAsync( Int32 companyId, Int32 offset, Int32 count );
		Task<Vendor> GetByIdAsync( Int32 id );
		Task<Vendor> CreateAsync( Vendor vendor );
		Task<Vendor> UpdateAsync( Vendor vendor );
		Task<Boolean> DeleteAsync( Int32 id );
	}
}
