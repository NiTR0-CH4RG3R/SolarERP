using backend.Models.Domains;

namespace backend.Repositories.Interfaces {
	public interface IRepositoryCompany {
		Task<IEnumerable<Company>> GetAllAsync();
		Task<Company> GetByIdAsync( Int32 id );
		Task<Company> CreateAsync( Company company );
		Task<Company> UpdateAsync( Company company );
		Task<Boolean> DeleteAsync( Int32 id );
	}
}
