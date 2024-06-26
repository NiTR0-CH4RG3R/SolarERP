﻿using backend.Models.Domains;

namespace backend.Repositories.Interfaces {
	public interface IRepositoryProject {
		Task<IEnumerable<Project>> GetAllByCompanyWithLimitAsync( Int32 companyId, Int32 offset, Int32 count );
		Task<IEnumerable<Project>> GetAllByCompanyAsync(Int32 companyId);
		Task<IEnumerable<Project>> GetAllByCustomerAsync(Int32 companyId, Int32 customerId);
		Task<Project> GetByIdAsync( Int32 id );
		Task<Project> CreateAsync( Project project );
		Task<Project> UpdateAsync( Project project );
		Task<Boolean> DeleteAsync( Int32 id );
	}
}
