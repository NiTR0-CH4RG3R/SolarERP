using backend.Models.Domains;
using backend.Models.DTO.VendorItem;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;

namespace backend.Services {
	public class ServiceVendorItem : IServiceVendorItem {
		IRepositoryParticipant _repositoryParticipant;
		IRepositorySystemUser _repositorySystemUser;
		IRepositoryVendor _repositoryVendor;
		IRepositoryVendorItem _repositoryVendorItem;
		ILogger<ServiceCustomer> _logger;

		public ServiceVendorItem( IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryVendor repositoryVendor, IRepositoryVendorItem repositoryVendorItem, ILogger<ServiceCustomer> logger ) {
			_repositoryParticipant = repositoryParticipant;
			_repositorySystemUser = repositorySystemUser;
			_repositoryVendor = repositoryVendor;
			_repositoryVendorItem = repositoryVendorItem;
			_logger = logger;
		}
		public async Task<GetVendorItemDTO> CreateAsync( int userId, AddVendorItemDTO vendorItem ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			Vendor? vendor = null;

			try {
				vendor = await _repositoryVendor.GetByIdAsync( vendorItem.VendorId );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			// Check if the vendor exists and if it belongs to the same company
			if ( vendor == null || vendor.CompanyId != companyId ) {
				throw new Exception( "Vendor not found" );
			}

			VendorItem vendorItemToCreate = new VendorItem {
				Id = null,
				Brand = vendorItem.Brand,
				Capacity = vendorItem.Capacity,
				VendorId = vendorItem.VendorId,
				Comments = vendorItem.Comments,
				Price = vendorItem.Price,
				WarrantyDuration	= vendorItem.WarrantyDuration,
				ProductCode = vendorItem.ProductCode,
				ProductName = vendorItem.ProductName,
				LastUpdatedBy = userId,
				LastUpdatedDateTime = null,
			};

			VendorItem? vendorItemCreated = null;

			try {
				vendorItemCreated = await _repositoryVendorItem.CreateAsync( vendorItemToCreate );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( vendorItemCreated == null ) {
				throw new Exception( "No vendor item was created" );
			}

			return new GetVendorItemDTO {
				Id = vendorItemCreated.Id,
				Brand = vendorItemCreated.Brand,
				Capacity = vendorItemCreated.Capacity,
				VendorId = vendorItemCreated.VendorId,
				Comments = vendorItemCreated.Comments,
				Price = vendorItemCreated.Price,
				WarrantyDuration = vendorItemCreated.WarrantyDuration,
				ProductCode = vendorItemCreated.ProductCode,
				ProductName = vendorItemCreated.ProductName,
				LastUpdatedBy = vendorItemCreated.LastUpdatedBy,
				LastUpdatedDateTime = vendorItemCreated.LastUpdatedDateTime,
			};
		}

		public async Task<bool> DeleteAsync( int userId, int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<GetVendorItemDTO>> GetAllAsync( int userId, int page, int pageSize ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			IEnumerable<VendorItem>? vendorItems = null;

			try {
				vendorItems = await _repositoryVendorItem.GetAllByCompanyWithLimitAsync( companyId, page, pageSize );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( vendorItems == null ) {
				throw new Exception( "No vendor items found" );
			}

			List<GetVendorItemDTO> vendorItemsDTO = new List<GetVendorItemDTO>();

			foreach ( VendorItem vendorItem in vendorItems ) {
				vendorItemsDTO.Add( new GetVendorItemDTO {
					Id = vendorItem.Id,
					Brand = vendorItem.Brand,
					Capacity = vendorItem.Capacity,
					VendorId = vendorItem.VendorId,
					Comments = vendorItem.Comments,
					Price = vendorItem.Price,
					WarrantyDuration = vendorItem.WarrantyDuration,
					ProductCode = vendorItem.ProductCode,
					ProductName = vendorItem.ProductName,
					LastUpdatedBy = vendorItem.LastUpdatedBy,
					LastUpdatedDateTime = vendorItem.LastUpdatedDateTime,
				} );
			}

			return vendorItemsDTO;
		}

		public async Task<GetVendorItemDTO> GetByIdAsync( int id ) {
			
			VendorItem? vendorItem = null;

			try {
				vendorItem = await _repositoryVendorItem.GetByIdAsync( id );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( vendorItem == null ) {
				throw new Exception( "No vendor item found" );
			}

			return new GetVendorItemDTO {
				Id = vendorItem.Id,
				Brand = vendorItem.Brand,
				Capacity = vendorItem.Capacity,
				VendorId = vendorItem.VendorId,
				Comments = vendorItem.Comments,
				Price = vendorItem.Price,
				WarrantyDuration = vendorItem.WarrantyDuration,
				ProductCode = vendorItem.ProductCode,
				ProductName = vendorItem.ProductName,
				LastUpdatedBy = vendorItem.LastUpdatedBy,
				LastUpdatedDateTime = vendorItem.LastUpdatedDateTime,
			};
		}

		public async Task<GetVendorItemDTO> UpdateAsync( int userId, int id, AddVendorItemDTO vendorItem ) {
			VendorItem vendorItemToUpdate = new VendorItem {
				Id = id,
				Brand = vendorItem.Brand,
				Capacity = vendorItem.Capacity,
				VendorId = vendorItem.VendorId,
				Comments = vendorItem.Comments,
				Price = vendorItem.Price,
				WarrantyDuration    = vendorItem.WarrantyDuration,
				ProductCode = vendorItem.ProductCode,
				ProductName = vendorItem.ProductName,
				LastUpdatedBy = userId,
				LastUpdatedDateTime = null,
			};

			VendorItem? vendorItemUpdated = null;

			try {
				vendorItemUpdated = await _repositoryVendorItem.UpdateAsync( vendorItemToUpdate );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( vendorItemUpdated == null ) {
				throw new Exception( "No vendor item was updated" );
			}

			return new GetVendorItemDTO {
				Id = vendorItemUpdated.Id,
				Brand = vendorItemUpdated.Brand,
				Capacity = vendorItemUpdated.Capacity,
				VendorId = vendorItemUpdated.VendorId,
				Comments = vendorItemUpdated.Comments,
				Price = vendorItemUpdated.Price,
				WarrantyDuration = vendorItemUpdated.WarrantyDuration,
				ProductCode = vendorItemUpdated.ProductCode,
				ProductName = vendorItemUpdated.ProductName,
				LastUpdatedBy = vendorItemUpdated.LastUpdatedBy,
				LastUpdatedDateTime = vendorItemUpdated.LastUpdatedDateTime,
			};
		}
	}
}
