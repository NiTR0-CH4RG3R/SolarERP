using AutoMapper;
using backend.Models.Domains;
using backend.Models.DTO.ProjectCommisionReport;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using FluentValidation;

namespace backend.Services {
	public class ServiceProjectCommisionReport( IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryProject repositoryProject, IRepositoryProjectCommisionReport repositoryProjectCommisionReport, IMapper mapper, ILogger<ServiceCustomer> logger, IValidator<AddProjectCommisionReportDTO> validator ) : IServiceProjectCommisionReport {

        IValidator<AddProjectCommisionReportDTO> _validator = validator;

        public async Task<GetProjectCommisionReportDTO> CreateAsync( int userId, AddProjectCommisionReportDTO projectCommisionReport ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( logger, repositoryParticipant, repositorySystemUser, userId );
			
			Project? project = null;

			try {
				project = await repositoryProject.GetByIdAsync( projectCommisionReport.ProjectId );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( project == null || project.CompanyId != companyId ) {
				throw new Exception( "Project not found" );
			}

			//Validation
			var validateResult = await _validator.ValidateAsync( projectCommisionReport );
			if ( !validateResult.IsValid )
			{
				throw new FluentValidation.ValidationException(validateResult.Errors);
			}

			ProjectCommisionReport projectCommisionReportToCreate = mapper.Map<ProjectCommisionReport>(projectCommisionReport);

			ProjectCommisionReport? projectCommisionReportCreated = null;

			try {
				projectCommisionReportCreated = await repositoryProjectCommisionReport.CreateAsync( projectCommisionReportToCreate );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( projectCommisionReportCreated == null ) {
				throw new Exception( "Project Commision Report not created" );
			}

			return mapper.Map<GetProjectCommisionReportDTO>( projectCommisionReportCreated );

		}

		public async Task<bool> DeleteAsync( int userId, int id ) {
			return await repositoryProjectCommisionReport.DeleteAsync( id );
		}



		public async Task<IEnumerable<GetProjectCommisionReportDTO>> GetAllByProjectAsync( int userId, int projectId, int page, int pageSize ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( logger, repositoryParticipant, repositorySystemUser, userId );

			Project? project = null;

			try {
				project = await repositoryProject.GetByIdAsync( projectId );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( project == null || project.CompanyId != companyId ) {
				throw new Exception( "Project not found" );
			}

			IEnumerable<ProjectCommisionReport>? projectCommisionReports = null;

			try {
				projectCommisionReports = await repositoryProjectCommisionReport.GetAllByProjectWithLimitAsync( projectId, ( page - 1 ) * pageSize, pageSize );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( projectCommisionReports == null ) {
				throw new Exception( "Project Commision Reports not found" );
			}

			return mapper.Map<IEnumerable<GetProjectCommisionReportDTO>>( projectCommisionReports );
		}

		public async Task<GetProjectCommisionReportDTO> GetByIdAsync( int id ) {
			
			ProjectCommisionReport? projectCommisionReport = null;

			try {
				projectCommisionReport = await repositoryProjectCommisionReport.GetByIdAsync( id );
			}
			catch ( Exception ex ) {
				logger.LogError( ex, ex.Message );
			}

			if ( projectCommisionReport == null ) {
				throw new Exception( "Project Commision Report not found" );
			}

			return mapper.Map<GetProjectCommisionReportDTO>( projectCommisionReport );
		}

		public async Task<GetProjectCommisionReportDTO> UpdateAsync( int userId, int id, AddProjectCommisionReportDTO projectCommisionReport ) {
			throw new NotImplementedException();
		}
	}
}
