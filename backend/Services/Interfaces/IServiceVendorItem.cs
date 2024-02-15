using backend.Models.DTO.VendorItem;

namespace backend.Services.Interfaces {
	public interface IServiceVendorItem {
		Task<IEnumerable<GetVendorItemDTO>> GetAllAsync( Int32 userId, Int32 page, Int32 pageSize );
		Task<GetVendorItemDTO> GetByIdAsync( Int32 id );
		Task<GetVendorItemDTO> CreateAsync( Int32 userId, AddVendorItemDTO vendorItem );
		Task<GetVendorItemDTO> UpdateAsync( Int32 userId, Int32 id, AddVendorItemDTO vendorItem );
		Task<Boolean> DeleteAsync( Int32 userId, Int32 id );
	}
}
