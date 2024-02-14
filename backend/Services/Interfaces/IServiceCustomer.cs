
using backend.Models.DTO.Customer;

namespace backend.Services.Interfaces {
	public interface IServiceCustomer {
		Task<IEnumerable<GetCustomerDTO>> GetAllAsync( Int32 userId );
		Task<IEnumerable<GetCustomerDTO>> GetAllByPagesAsync( Int32 userId, Int32 page, Int32 pageSize );
		Task<GetCustomerDTO> GetByIdAsync( Int32 id );
		Task<GetCustomerDTO> CreateAsync( Int32 userId, AddCustomerDTO customer );
		Task<GetCustomerDTO> UpdateAsync( Int32 userId, Int32 id, AddCustomerDTO customer );
		Task<Boolean> DeleteAsync( Int32 userId, Int32 id );
	}
}
