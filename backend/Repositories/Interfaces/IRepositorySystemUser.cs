using backend.Models.Domains;

namespace backend.Repositories.Interfaces {
	public interface IRepositorySystemUser {
		Task<IEnumerable<SystemUser>> GetAllByCompanyAsync( Int32 companyId );
		Task<IEnumerable<SystemUser>> GetAllByCompanyWithLimitAsync( Int32 companyId, Int32 offset, Int32 count );
		Task<SystemUser> GetByIdAsync( Int32 id );
		Task<SystemUser> GetByUsernameAndPasswordAsync( String username, String password );
		Task<SystemUser> CreateAsync( SystemUser systemUser );
		Task<SystemUser> UpdateAsync( SystemUser systemUser );
		Task<Boolean> DeleteAsync( Int32 id );

		Task<SystemUserLogin> InsertSystemUserLoginAsync( SystemUserLogin systemUserLogin );
		Task<SystemUserLogin> GetSystemUserLoginByIdAsync( Int32 id );
		Task<SystemUserLogin> UpdateSystemUserLoginAccessTokenAsync( Int32 id, String accessToken );
		void DeleteSystemUserLoginByIdAsync( Int32 id );
	}

}
