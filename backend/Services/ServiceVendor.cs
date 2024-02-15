using backend.Models.Domains;
using backend.Models.DTO.Vendor;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;

namespace backend.Services {
	public class ServiceVendor : IServiceVendor {

		IRepositoryParticipant _repositoryParticipant;
		IRepositorySystemUser _repositorySystemUser;
		IRepositoryVendor _repositoryVendor;
		ILogger<ServiceCustomer> _logger;

		public ServiceVendor( IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryVendor repositoryVendor, ILogger<ServiceCustomer> logger  ) {
			_repositoryParticipant = repositoryParticipant;
			_repositorySystemUser = repositorySystemUser;
			_repositoryVendor = repositoryVendor;
			_logger = logger;
		}
		public async Task<GetVendorDTO> CreateAsync( int userId, AddVendorDTO vendor ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			Vendor vendorToCreate = new Vendor {
				CompanyId = companyId,
				Name = vendor.Name,
				Address = vendor.Address,
				Email = vendor.Email,
				Phone01 = vendor.Phone01,
				Phone02 = vendor.Phone02,
				VendorRegistrationNumber = vendor.VendorRegistrationNumber,
				Comments = vendor.Comments,
				LastUpdatedBy = userId,
				LastUpdatedDateTime = null
			};

			Vendor? vendorCreated = null;

			try {
				vendorCreated = await _repositoryVendor.CreateAsync( vendorToCreate );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( vendorCreated == null ) {
				throw new Exception( "No vendor was created" );
			}

			return new GetVendorDTO {
				Id = vendorCreated.Id,
				Name = vendorCreated.Name,
				Address = vendorCreated.Address,
				Email = vendorCreated.Email,
				Phone01 = vendorCreated.Phone01,
				Phone02 = vendorCreated.Phone02,
				VendorRegistrationNumber = vendorCreated.VendorRegistrationNumber,
				Comments = vendorCreated.Comments,
				LastUpdatedBy = vendorCreated.LastUpdatedBy,
				LastUpdatedDateTime = vendorCreated.LastUpdatedDateTime
			};
		}

		public async Task<bool> DeleteAsync( int userId, int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<GetVendorDTO>> GetAllAsync( int userId, int page, int pageSize ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			IEnumerable<Vendor>? vendors = null;

			try { 
				vendors = await _repositoryVendor.GetAllByCompanyWithLimitAsync( companyId, page, pageSize );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( vendors == null ) {
				throw new Exception( "No vendors found" );
			}

			List<GetVendorDTO> result = new List<GetVendorDTO>();

			foreach ( Vendor vendor in vendors ) {
				result.Add( new GetVendorDTO {
					Id = vendor.Id,
					Name = vendor.Name,
					Address = vendor.Address,
					Email = vendor.Email,
					Phone01 = vendor.Phone01,
					Phone02 = vendor.Phone02,
					VendorRegistrationNumber = vendor.VendorRegistrationNumber,
					Comments = vendor.Comments,
					LastUpdatedBy = vendor.LastUpdatedBy,
					LastUpdatedDateTime = vendor.LastUpdatedDateTime
				} );
			}

			return result;
		}

		public async Task<GetVendorDTO> GetByIdAsync( int id ) {
			Vendor? vendor = null;

			try {
				vendor = await _repositoryVendor.GetByIdAsync( id );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( vendor == null ) {
				throw new Exception( "No vendor found" );
			}

			return new GetVendorDTO {
				Id = vendor.Id,
				Name = vendor.Name,
				Address = vendor.Address,
				Email = vendor.Email,
				Phone01 = vendor.Phone01,
				Phone02 = vendor.Phone02,
				VendorRegistrationNumber = vendor.VendorRegistrationNumber,
				Comments = vendor.Comments,
				LastUpdatedBy = vendor.LastUpdatedBy,
				LastUpdatedDateTime = vendor.LastUpdatedDateTime
			};
		}

		public async Task<GetVendorDTO> UpdateAsync( int userId, int id, AddVendorDTO vendor ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			Vendor vendorToUpdate = new Vendor {
				Id = id,
				CompanyId = companyId,
				Name = vendor.Name,
				Address = vendor.Address,
				Email = vendor.Email,
				Phone01 = vendor.Phone01,
				Phone02 = vendor.Phone02,
				VendorRegistrationNumber = vendor.VendorRegistrationNumber,
				Comments = vendor.Comments,
				LastUpdatedBy = userId,
				LastUpdatedDateTime = null
			};

			Vendor? vendorUpdated = null;

			try {
				vendorUpdated = await _repositoryVendor.UpdateAsync( vendorToUpdate );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( vendorUpdated == null ) {
				throw new Exception( "No vendor was updated" );
			}

			return new GetVendorDTO {
				Id = vendorUpdated.Id,
				Name = vendorUpdated.Name,
				Address = vendorUpdated.Address,
				Email = vendorUpdated.Email,
				Phone01 = vendorUpdated.Phone01,
				Phone02 = vendorUpdated.Phone02,
				VendorRegistrationNumber = vendorUpdated.VendorRegistrationNumber,
				Comments = vendorUpdated.Comments,
				LastUpdatedBy = vendorUpdated.LastUpdatedBy,
				LastUpdatedDateTime = vendorUpdated.LastUpdatedDateTime
			};
		}
	}
}
