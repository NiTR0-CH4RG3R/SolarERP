using backend.Models.Domains;

namespace backend.Repositories.Interfaces {
	public interface IRepositoryVendorItem {
		Task<IEnumerable<VendorItem>> GetAllByVendorWithLimitAsync( Int32 vendorId, Int32 offset, Int32 count );
		Task<IEnumerable<VendorItem>> GetAllByCompanyWithLimitAsync( Int32 companyId, Int32 offset, Int32 count );
		Task<VendorItem> GetByIdAsync( Int32 id );
		Task<VendorItem> CreateAsync( VendorItem vendorItem );
		Task<VendorItem> UpdateAsync( VendorItem vendorItem );
		Task<Boolean> DeleteAsync( Int32 id );
	}
}
