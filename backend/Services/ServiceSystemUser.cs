using backend.Models.Domains;
using backend.Models.DTO.SystemUser;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;

namespace backend.Services {
	public class ServiceSystemUser : IServiceSystemUser {

		IRepositoryParticipant _repositoryParticipant;
		IRepositorySystemUser _repositorySystemUser;
		ILogger<ServiceCustomer> _logger;

        public ServiceSystemUser( IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, ILogger<ServiceCustomer> logger ) {
			_repositoryParticipant = repositoryParticipant;
			_repositorySystemUser = repositorySystemUser;
			_logger = logger;
        }

		public async Task<GetSystemUserDTO> CreateAsync( int userId, AddSystemUserDTO systemUser ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);



            Participant participantToCreate = new Participant {
				FirstName = systemUser.FirstName,
				LastName = systemUser.LastName,
				Category = ParticipantCategory.Employee.ToString(),
				Address = systemUser.Address,
				Email = systemUser.Email,
				Phone01 = systemUser.Phone01,
				Phone02 = systemUser.Phone02,
				Profession = systemUser.Profession,
				CustomerRegistrationNumber = systemUser.CustomerRegistrationNumber,
				Comments = systemUser.Comments,
				CompanyId = companyId,
				LastUpdatedBy = userId,
			};

			Participant? participantCreated = null;

			try {
				participantCreated = await _repositoryParticipant.CreateAsync(participantToCreate);
			}
			catch (Exception ex) {
				_logger.LogError(ex, ex.Message);
			}

			if (participantCreated == null) {
				throw new Exception("No participant was created");
			}

			SystemUser systemUserToCreate = new SystemUser {
				Id = participantCreated.Id,
				Username = systemUser.Username,
				Password = systemUser.Password,
				Role = systemUser.Role,
				ProfilePicture = systemUser.ProfilePicture,
				LastUpdatedBy = userId,
			};

			SystemUser? systemUserCreated = null;

			try {
				systemUserCreated = await _repositorySystemUser.CreateAsync(systemUserToCreate);
			}
			catch (Exception ex) {
				_logger.LogError(ex, ex.Message);
			}

			if (systemUserCreated == null) {
				throw new Exception("No system user was created");
			}

			return new GetSystemUserDTO {
				Id = systemUserCreated.Id,
				FirstName = participantCreated.FirstName,
				LastName = participantCreated.LastName,
				Email = participantCreated.Email,
				Phone01 = participantCreated.Phone01,
				Phone02 = participantCreated.Phone02,
				Address = participantCreated.Address,
				Profession = participantCreated.Profession,
				CustomerRegistrationNumber = participantCreated.CustomerRegistrationNumber,
				Comments = participantCreated.Comments,
				Password = systemUserCreated.Password,
				Username = systemUserCreated.Username,
				Role = systemUserCreated.Role,
				ProfilePicture = systemUserCreated.ProfilePicture,
				LastUpdatedBy = systemUserCreated.LastUpdatedBy,
				LastUpdatedDateTime = systemUserCreated.LastUpdatedDateTime,
			};
		}

		public async Task<bool> DeleteAsync( int userId, int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<GetSystemUserDTO>> GetAllAsync( Int32 userId ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

			IEnumerable<SystemUser>? systemUsers = null;

			try {
				systemUsers = await _repositorySystemUser.GetAllByCompanyAsync( companyId );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( systemUsers == null ) {
				throw new Exception( "No data found" );
			}

			List<GetSystemUserDTO> systemUsersDTO = new List<GetSystemUserDTO>();

			foreach ( SystemUser systemUser in systemUsers ) {
				Participant? participant = null;

				try {
					participant = await _repositoryParticipant.GetByIdAsync( ( int )systemUser.Id );
				}
				catch ( Exception ex ) {
					_logger.LogError( ex, ex.Message );
				}

				if ( participant == null ) {
					throw new Exception( "No data found" );
				}

				systemUsersDTO.Add( new GetSystemUserDTO {
					Id = systemUser.Id,
					FirstName = participant.FirstName,
					LastName = participant.LastName,
					Email = participant.Email,
					Phone01 = participant.Phone01,
					Phone02 = participant.Phone02,
					Address = participant.Address,
					Profession = participant.Profession,
					CustomerRegistrationNumber = participant.CustomerRegistrationNumber,
					Comments = participant.Comments,
					Password = systemUser.Password,
					Username = systemUser.Username,
					Role = systemUser.Role,
					ProfilePicture = systemUser.ProfilePicture,
					LastUpdatedBy = systemUser.LastUpdatedBy,
					LastUpdatedDateTime = systemUser.LastUpdatedDateTime,
				} );
			}

			return systemUsersDTO;
		}

		public async Task<IEnumerable<GetSystemUserDTO>> GetAllWithLimitAsync( int userId, int page, int pageSize ) {
			
			Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

			IEnumerable<SystemUser>? systemUsers = null;

			try {
				systemUsers = await _repositorySystemUser.GetAllByCompanyWithLimitAsync(companyId, ( page - 1 ) * pageSize, pageSize);
			}
			catch (Exception ex) {
				_logger.LogError(ex, ex.Message);
			}

			if (systemUsers == null) {
				throw new Exception("No data found");
			}

			List<GetSystemUserDTO> systemUsersDTO = new List<GetSystemUserDTO>();

			foreach (SystemUser systemUser in systemUsers) {
				Participant? participant = null;

				try {
					participant = await _repositoryParticipant.GetByIdAsync((int)systemUser.Id);
				}
				catch (Exception ex) {
					_logger.LogError(ex, ex.Message);
				}

				if (participant == null) {
					throw new Exception("No data found");
				}

				systemUsersDTO.Add(new GetSystemUserDTO {
					Id = systemUser.Id,
					FirstName = participant.FirstName,
					LastName = participant.LastName,
					Email = participant.Email,
					Phone01 = participant.Phone01,
					Phone02 = participant.Phone02,
					Address = participant.Address,
					Profession = participant.Profession,
					CustomerRegistrationNumber = participant.CustomerRegistrationNumber,
					Comments = participant.Comments,
					Password = systemUser.Password,
					Username = systemUser.Username,
					Role = systemUser.Role,
					ProfilePicture = systemUser.ProfilePicture,
					LastUpdatedBy = systemUser.LastUpdatedBy,
					LastUpdatedDateTime = systemUser.LastUpdatedDateTime,
				});
			}

			return systemUsersDTO;
		}

		public async Task<GetSystemUserDTO> GetByIdAsync( int id ) {
			
			SystemUser? systemUser = null;

			try {
				systemUser = await _repositorySystemUser.GetByIdAsync(id);
			}
			catch (Exception ex) {
				_logger.LogError(ex, ex.Message);
			}

			if (systemUser == null) {
				throw new Exception("No data found");
			}

			Participant? participant = null;

			try {
				participant = await _repositoryParticipant.GetByIdAsync((int)systemUser.Id);
			}
			catch (Exception ex) {
				_logger.LogError(ex, ex.Message);
			}

			if (participant == null) {
				throw new Exception("No data found");
			}

			return new GetSystemUserDTO {
				Id = systemUser.Id,
				FirstName = participant.FirstName,
				LastName = participant.LastName,
				Email = participant.Email,
				Phone01 = participant.Phone01,
				Phone02 = participant.Phone02,
				Address = participant.Address,
				Profession = participant.Profession,
				CustomerRegistrationNumber = participant.CustomerRegistrationNumber,
				Comments = participant.Comments,
				Password = "",
				Username = systemUser.Username,
				Role = systemUser.Role,
				ProfilePicture = systemUser.ProfilePicture,
				LastUpdatedBy = systemUser.LastUpdatedBy,
				LastUpdatedDateTime = systemUser.LastUpdatedDateTime,
			};
		}

		public async Task<GetSystemUserDTO> UpdateAsync( int userId, int id, AddSystemUserDTO systemUser ) {
			
			Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);



            Participant participantToUpdate = new Participant {
				Id = id,
				FirstName = systemUser.FirstName,
				LastName = systemUser.LastName,
				Category = ParticipantCategory.Employee.ToString(),
				Address = systemUser.Address,
				Email = systemUser.Email,
				Phone01 = systemUser.Phone01,
				Phone02 = systemUser.Phone02,
				Profession = systemUser.Profession,
				CustomerRegistrationNumber = systemUser.CustomerRegistrationNumber,
				Comments = systemUser.Comments,
				CompanyId = companyId,
				LastUpdatedBy = userId,
			};

			Participant? participantUpdated = null;

			try {
				participantUpdated = await _repositoryParticipant.UpdateAsync(participantToUpdate);
			}
			catch (Exception ex) {
				_logger.LogError(ex, ex.Message);
			}

			if (participantUpdated == null) {
				throw new Exception("No participant was updated");
			}

			SystemUser systemUserToUpdate = new SystemUser {
				Id = id,
				Username = systemUser.Username,
				Password = systemUser.Password,
				Role = systemUser.Role,
				ProfilePicture = systemUser.ProfilePicture,
				LastUpdatedBy = userId,
			};

			SystemUser? systemUserUpdated = null;

			try {
				systemUserUpdated = await _repositorySystemUser.UpdateAsync(systemUserToUpdate);
			}
			catch (Exception ex) {
				_logger.LogError(ex, ex.Message);
			}

			if (systemUserUpdated == null) {
				throw new Exception("No system user was updated");
			}

			return new GetSystemUserDTO {
				Id = systemUserUpdated.Id,
				FirstName = participantUpdated.FirstName,
				LastName = participantUpdated.LastName,
				Email = participantUpdated.Email,
				Phone01 = participantUpdated.Phone01,
				Phone02 = participantUpdated.Phone02,
				Address = participantUpdated.Address,
				Profession = participantUpdated.Profession,
				CustomerRegistrationNumber = participantUpdated.CustomerRegistrationNumber,
				Comments = participantUpdated.Comments,
				Password = systemUserUpdated.Password,
				Username = systemUserUpdated.Username,
				Role = systemUserUpdated.Role,
				ProfilePicture = systemUserUpdated.ProfilePicture,
				LastUpdatedBy = systemUserUpdated.LastUpdatedBy,
				LastUpdatedDateTime = systemUserUpdated.LastUpdatedDateTime,
			};
		}
	}
}
