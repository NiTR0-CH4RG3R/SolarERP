using backend.Models.Domains;
using backend.Models.DTO.Customer;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backend.Services {
	public class ServiceCustomer : IServiceCustomer {

		IRepositoryParticipant _repositoryParticipant;
		IRepositorySystemUser _repositorySystemUser;
		ILogger<ServiceCustomer> _logger;

		public ServiceCustomer( IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, ILogger<ServiceCustomer> logger ) {
			_repositoryParticipant = repositoryParticipant;
			_repositorySystemUser = repositorySystemUser;
			_logger = logger;
		}

		public async Task<GetCustomerDTO> CreateAsync( int userId, AddCustomerDTO customer ) {
			// Check if the user is an employee
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			// Create the customer
			Participant newCustomer = new Participant {
				CompanyId = companyId,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Category = customer.Category,
				Address = customer.Address,
				Email = customer.Email,
				Phone01 = customer.Phone01,
				Phone02 = customer.Phone02,
				CustomerRegistrationNumber = customer.CustomerRegistrationNumber,
				Profession = customer.Profession,
				Comments = customer.Comments,
				LastUpdatedBy = userId,
			};

			Participant? result = null;

			try {
				result = await _repositoryParticipant.CreateAsync( newCustomer );
			}
			catch ( Exception e ) {
				_logger.LogError( e, "Error while trying to create a new customer" );
			}

			if ( result == null ) {
				throw new Exception( "Customer not created" );
			}

			return new GetCustomerDTO {
				Id = result.Id,
				FirstName = result.FirstName,
				LastName = result.LastName,
				Category = result.Category,
				Address = result.Address,
				Email = result.Email,
				Phone01 = result.Phone01,
				Phone02 = result.Phone02,
				CustomerRegistrationNumber = result.CustomerRegistrationNumber,
				Profession = result.Profession,
				Comments = result.Comments,
				LastUpdatedBy = result.LastUpdatedBy,
				LastUpdatedDateTime = result.LastUpdatedDateTime,
			};

		}

		public async Task<bool> DeleteAsync( int userId, int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<GetCustomerDTO>> GetAllAsync( int userId ) {

			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );
			IEnumerable<Participant>? results = null;

			try {
				results = await _repositoryParticipant.GetAllByCompanyAsync( companyId );
			}
			catch ( Exception e ) {
				_logger.LogError( e, "Error while trying to get customers by pages" );
			}

			if ( results == null ) {
				throw new Exception( "No customers found" );
			}

			List<GetCustomerDTO> customers = new List<GetCustomerDTO>();

			foreach ( var result in results ) {
				customers.Add( new GetCustomerDTO {
					Id = result.Id,
					FirstName = result.FirstName,
					LastName = result.LastName,
					Category = result.Category,
					Address = result.Address,
					Email = result.Email,
					Phone01 = result.Phone01,
					Phone02 = result.Phone02,
					CustomerRegistrationNumber = result.CustomerRegistrationNumber,
					Profession = result.Profession,
					Comments = result.Comments,
					LastUpdatedBy = result.LastUpdatedBy,
					LastUpdatedDateTime = result.LastUpdatedDateTime,
				} );
			}

			return customers;
		}

		public async Task<IEnumerable<GetCustomerDTO>> GetAllByPagesAsync( int userId, int page, int pageSize ) {

			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );
			IEnumerable<Participant>? results = null;

			try {
				results = await _repositoryParticipant.GetAllNonEmlployeesByCompanyWithLimitAsync( companyId, ( page - 1 ) * pageSize, pageSize );
			}
			catch ( Exception e ) {
				_logger.LogError( e, "Error while trying to get customers by pages" );
			}

			if ( results == null ) {
				throw new Exception( "No customers found" );
			}

			List<GetCustomerDTO> customers = new List<GetCustomerDTO>();

			foreach ( var result in results ) {
				customers.Add( new GetCustomerDTO {
					Id = result.Id,
					FirstName = result.FirstName,
					LastName = result.LastName,
					Category = result.Category,
					Address = result.Address,
					Email = result.Email,
					Phone01 = result.Phone01,
					Phone02 = result.Phone02,
					CustomerRegistrationNumber = result.CustomerRegistrationNumber,
					Profession = result.Profession,
					Comments = result.Comments,
					LastUpdatedBy = result.LastUpdatedBy,
					LastUpdatedDateTime = result.LastUpdatedDateTime,
				} );
			}

			return customers;
		}

		public async Task<GetCustomerDTO> GetByIdAsync( int id ) {
			Participant? result = null;

			try {
				result = await _repositoryParticipant.GetByIdAsync( id );
			}
			catch ( Exception e ) {
				_logger.LogError( e, "Error while trying to get a customer by id" );
			}

			if ( result == null ) {
				throw new Exception( "No customer found" );
			}

			return new GetCustomerDTO {
				Id = result.Id,
				FirstName = result.FirstName,
				LastName = result.LastName,
				Category = result.Category,
				Address = result.Address,
				Email = result.Email,
				Phone01 = result.Phone01,
				Phone02 = result.Phone02,
				CustomerRegistrationNumber = result.CustomerRegistrationNumber,
				Profession = result.Profession,
				Comments = result.Comments,
				LastUpdatedBy = result.LastUpdatedBy,
				LastUpdatedDateTime = result.LastUpdatedDateTime,
			};
		}

		public async Task<GetCustomerDTO> UpdateAsync( int userId, int id, AddCustomerDTO customer ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			Participant updatedCustomer = new Participant {
				Id = id,
				CompanyId = companyId,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Category = customer.Category,
				Address = customer.Address,
				Email = customer.Email,
				Phone01 = customer.Phone01,
				Phone02 = customer.Phone02,
				CustomerRegistrationNumber = customer.CustomerRegistrationNumber,
				Profession = customer.Profession,
				Comments = customer.Comments,
				LastUpdatedBy = userId,
			};

			Participant? result = null;

			try {
				result = await _repositoryParticipant.UpdateAsync( updatedCustomer );
			}
			catch ( Exception e ) {
				_logger.LogError( e, "Error while trying to update a customer" );
			}

			if ( result == null ) {
				throw new Exception( "Customer not updated" );
			}

			return new GetCustomerDTO {
				Id = result.Id,
				FirstName = result.FirstName,
				LastName = result.LastName,
				Category = result.Category,
				Address = result.Address,
				Email = result.Email,
				Phone01 = result.Phone01,
				Phone02 = result.Phone02,
				CustomerRegistrationNumber = result.CustomerRegistrationNumber,
				Profession = result.Profession,
				Comments = result.Comments,
				LastUpdatedBy = result.LastUpdatedBy,
				LastUpdatedDateTime = result.LastUpdatedDateTime,
			};
		}
	}
}
