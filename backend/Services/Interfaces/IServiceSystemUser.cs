using backend.Models.DTO.SystemUser;

namespace backend.Services.Interfaces {
	public interface IServiceSystemUser {

		Task<IEnumerable<GetSystemUserDTO>> GetAllAsync( Int32 userId );
		Task<IEnumerable<GetSystemUserDTO>> GetAllWithLimitAsync( Int32 userId, Int32 page, Int32 pageSize );
		Task<GetSystemUserDTO> GetByIdAsync( Int32 id );
		Task<GetSystemUserDTO> CreateAsync( Int32 userId, AddSystemUserDTO systemUser );
		Task<GetSystemUserDTO> UpdateAsync( Int32 userId, Int32 id, AddSystemUserDTO systemUser );
		Task<Boolean> DeleteAsync( Int32 userId, Int32 id );
	}
}
