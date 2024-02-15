using backend.Models.DTO.Vendor;

namespace backend.Services.Interfaces {
	public interface IServiceVendor {
		Task<IEnumerable<GetVendorDTO>> GetAllAsync( Int32 userId, Int32 page, Int32 pageSize );
		Task<GetVendorDTO> GetByIdAsync( Int32 id );
		Task<GetVendorDTO> CreateAsync( Int32 userId, AddVendorDTO vendor );
		Task<GetVendorDTO> UpdateAsync( Int32 userId, Int32 id, AddVendorDTO vendor );
		Task<Boolean> DeleteAsync( Int32 userId, Int32 id );
	}
}
