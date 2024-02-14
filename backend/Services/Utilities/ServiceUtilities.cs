using backend.Models.Domains;
using backend.Repositories;
using backend.Repositories.Interfaces;

namespace backend.Services.Utilities {
	public class ServiceUtilities {
		static public async Task<Int32> GetCompanyId( ILogger logger, IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, Int32 userId ) {
			// Check if the user is an employee
			SystemUser? user = null;

			try {
				user = await repositorySystemUser.GetByIdAsync( userId );
			}
			catch ( Exception e ) {
				logger.LogError( e, "Error while trying to get user by id {userId}", userId );
			}

			if ( user == null ) {
				throw new Exception( "User is not an employee" );
			}
			Participant? participant = null;

			try {
				participant = await repositoryParticipant.GetByIdAsync( userId );
			}
			catch ( Exception e ) {
				logger.LogError( e, "Error while trying to get participant by id {participantId}", userId );
			}

			if ( participant == null ) {
				throw new Exception( "Participant not found" );
			}

			if ( participant.CompanyId == null ) {
				throw new Exception( "Given user doesn't have a company id associated with them" );
			}

			return (int)participant.CompanyId;
		}
	}
}
