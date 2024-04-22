using AutoMapper;
using backend.Models.Domains;
using backend.Models.DTO.Customer;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;


namespace backend.Services {
	public class ServiceCustomer( IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IMapper mapper, ILogger<ServiceCustomer> logger ) : IServiceCustomer {

		IRepositoryParticipant _repositoryParticipant = repositoryParticipant;
		IRepositorySystemUser _repositorySystemUser = repositorySystemUser;
		IMapper _mapper = mapper;
		ILogger<ServiceCustomer> _logger = logger;

		public async Task<GetCustomerDTO> CreateAsync( int userId, AddCustomerDTO customer ) {
			// Check if the user is an employee
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			Participant newCustomer = _mapper.Map<Participant>( customer );
			newCustomer.CompanyId = companyId;

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

			return _mapper.Map<GetCustomerDTO>( result );

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

			var customers = _mapper.Map<IEnumerable<GetCustomerDTO>>( results );

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

			List<GetCustomerDTO> customers = _mapper.Map<List<GetCustomerDTO>>( results );

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

			return _mapper.Map<GetCustomerDTO>(result);
		}

		public async Task<GetCustomerDTO> UpdateAsync( int userId, int id, AddCustomerDTO customer ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			Participant updatedCustomer = _mapper.Map<Participant>( customer );
			updatedCustomer.Id = id;

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

			return _mapper.Map< GetCustomerDTO>(result);
		}
	}
}
