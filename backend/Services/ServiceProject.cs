using backend.Models.Domains;
using backend.Models.DTO.Project;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.Services.Utilities;
using FluentValidation;

namespace backend.Services {
	public class ServiceProject : IServiceProject {

		IRepositoryParticipant _repositoryParticipant;
		IRepositorySystemUser _repositorySystemUser;
		IRepositoryProject _repositoryProject;
		ILogger<ServiceCustomer> _logger;

        public ServiceProject( IRepositoryParticipant repositoryParticipant, IRepositorySystemUser repositorySystemUser, IRepositoryProject repositoryProject, ILogger<ServiceCustomer> logger ) {
			_repositoryParticipant = repositoryParticipant;
			_repositorySystemUser = repositorySystemUser;
			_repositoryProject = repositoryProject;
			_logger = logger;
        }

		public async Task<GetProjectDTO> CreateAsync( int userId, AddProjectDTO project ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			

			Project projectToCreate = new Project {
				Id = null,
				CompanyId = companyId,
				Address = project.Address,
				Comments = project.Comments,
				Description = project.Description,
				LastUpdatedBy = userId,
				LastUpdatedDateTime = null,
				CommissionDate = project.CommissionDate,
				CoordinatorId = project.CoordinatorId,
				CustomerId = project.CustomerId,
				ElectricityAccountNumber = project.ElectricityAccountNumber,
				ElectricityBoardArea = project.ElectricityBoardArea,
				ElectricityTariffStructure = project.ElectricityTariffStructure,
				EstimatedCost = project.EstimatedCost,
				LocationCoordinates = project.LocationCoordinates,
				ProjectIdentificationNumber = project.ProjectIdentificationNumber,
				ReferencedBy = project.ReferencedBy,
				SalesPerson = project.SalesPerson,
				StartDate = project.StartDate,
				Status = project.Status,
				SystemWarrantyPeriod = project.SystemWarrantyPeriod
			};

			Project? projectCreated = null;

			try {
				projectCreated = await _repositoryProject.CreateAsync( projectToCreate );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( projectCreated == null ) {
				throw new Exception( "Project not created" );
			}

			GetProjectDTO projectDTO = new GetProjectDTO {
				Id = projectCreated.Id,
				Address = projectCreated.Address,
				Comments = projectCreated.Comments,
				Description = projectCreated.Description,
				CommissionDate = projectCreated.CommissionDate,
				CoordinatorId = projectCreated.CoordinatorId,
				CustomerId = projectCreated.CustomerId,
				ElectricityAccountNumber = projectCreated.ElectricityAccountNumber,
				ElectricityBoardArea = projectCreated.ElectricityBoardArea,
				ElectricityTariffStructure = projectCreated.ElectricityTariffStructure,
				EstimatedCost = projectCreated.EstimatedCost,
				LocationCoordinates = projectCreated.LocationCoordinates,
				ProjectIdentificationNumber = projectCreated.ProjectIdentificationNumber,
				ReferencedBy = projectCreated.ReferencedBy,
				SalesPerson = projectCreated.SalesPerson,
				StartDate = projectCreated.StartDate,
				Status = projectCreated.Status,
				SystemWarrantyPeriod = projectCreated.SystemWarrantyPeriod
			};

			return projectDTO;
		}

		public async Task<bool> DeleteAsync( int userId, int id ) {
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<GetProjectDTO>> GetAllAsync(Int32 userId)
		{
			Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

			IEnumerable<Project>? projects = null;

			try
			{
				projects = await _repositoryProject.GetAllByCompanyAsync(companyId);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
			}

			if (projects == null)
			{
				throw new Exception("No data found");
			}

			List<GetProjectDTO> projectDTOs = new List<GetProjectDTO>();

			foreach (Project project in projects)
			{
				GetProjectDTO projectDTO = new GetProjectDTO
				{
					Id = project.Id,
					Address = project.Address,
					Comments = project.Comments,
					Description = project.Description,
					CommissionDate = project.CommissionDate,
					CoordinatorId = project.CoordinatorId,
					CustomerId = project.CustomerId,
					ElectricityAccountNumber = project.ElectricityAccountNumber,
					ElectricityBoardArea = project.ElectricityBoardArea,
					ElectricityTariffStructure = project.ElectricityTariffStructure,
					EstimatedCost = project.EstimatedCost,
					LocationCoordinates = project.LocationCoordinates,
					ProjectIdentificationNumber = project.ProjectIdentificationNumber,
					ReferencedBy = project.ReferencedBy,
					SalesPerson = project.SalesPerson,
					StartDate = project.StartDate,
					Status = project.Status,
					SystemWarrantyPeriod = project.SystemWarrantyPeriod
				};

				projectDTOs.Add(projectDTO);


			}
			return projectDTOs;

		}

        public async Task<IEnumerable<GetProjectDTO>> GetAllByCustomerId(int userId, int customerId)
        {
            Int32 companyId = await ServiceUtilities.GetCompanyId(_logger, _repositoryParticipant, _repositorySystemUser, userId);

			IEnumerable<Project>? projects = null;

			try
			{
				projects = await _repositoryProject.GetAllByCustomerAsync(companyId, customerId);

			}catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
			}

			if(projects == null)
			{
                throw new Exception("No data found");
            }

			List<GetProjectDTO> projectDTOs = new List<GetProjectDTO>();

			foreach (Project project in projects)
			{
				GetProjectDTO projectDTO = new GetProjectDTO {
                    Id = project.Id,
                    Address = project.Address,
                    Comments = project.Comments,
                    Description = project.Description,
                    CommissionDate = project.CommissionDate,
                    CoordinatorId = project.CoordinatorId,
                    CustomerId = project.CustomerId,
                    ElectricityAccountNumber = project.ElectricityAccountNumber,
                    ElectricityBoardArea = project.ElectricityBoardArea,
                    ElectricityTariffStructure = project.ElectricityTariffStructure,
                    EstimatedCost = project.EstimatedCost,
                    LocationCoordinates = project.LocationCoordinates,
                    ProjectIdentificationNumber = project.ProjectIdentificationNumber,
                    ReferencedBy = project.ReferencedBy,
                    SalesPerson = project.SalesPerson,
                    StartDate = project.StartDate,
                    Status = project.Status,
                    SystemWarrantyPeriod = project.SystemWarrantyPeriod
                };
                projectDTOs.Add(projectDTO);

            }
            return projectDTOs;

        }

        public async Task<IEnumerable<GetProjectDTO>> GetAllWithLimitAsync( int userId, int page, int pageSize ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );

			IEnumerable<Project>? projects = null;

			try {
				projects = await _repositoryProject.GetAllByCompanyWithLimitAsync( companyId, ( page - 1 ) * pageSize, pageSize );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( projects == null ) {
				throw new Exception( "No data found" );
			}

			List<GetProjectDTO> projectDTOs = new List<GetProjectDTO>();

			foreach ( Project project in projects ) {
				GetProjectDTO projectDTO = new GetProjectDTO {
					Id = project.Id,
					Address = project.Address,
					Comments = project.Comments,
					Description = project.Description,
					CommissionDate = project.CommissionDate,
					CoordinatorId = project.CoordinatorId,
					CustomerId = project.CustomerId,
					ElectricityAccountNumber = project.ElectricityAccountNumber,
					ElectricityBoardArea = project.ElectricityBoardArea,
					ElectricityTariffStructure = project.ElectricityTariffStructure,
					EstimatedCost = project.EstimatedCost,
					LocationCoordinates = project.LocationCoordinates,
					ProjectIdentificationNumber = project.ProjectIdentificationNumber,
					ReferencedBy = project.ReferencedBy,
					SalesPerson = project.SalesPerson,
					StartDate = project.StartDate,
					Status = project.Status,
					SystemWarrantyPeriod = project.SystemWarrantyPeriod
				};

				projectDTOs.Add( projectDTO );
			}

			return projectDTOs;

		}

		public async Task<GetProjectDTO> GetByIdAsync( int id ) {
			Project? project = null;

			try {
				project = await _repositoryProject.GetByIdAsync( id );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( project == null ) {
				throw new Exception( "No data found" );
			}

			GetProjectDTO projectDTO = new GetProjectDTO {
				Id = project.Id,
				Address = project.Address,
				Comments = project.Comments,
				Description = project.Description,
				CommissionDate = project.CommissionDate,
				CoordinatorId = project.CoordinatorId,
				CustomerId = project.CustomerId,
				ElectricityAccountNumber = project.ElectricityAccountNumber,
				ElectricityBoardArea = project.ElectricityBoardArea,
				ElectricityTariffStructure = project.ElectricityTariffStructure,
				EstimatedCost = project.EstimatedCost,
				LocationCoordinates = project.LocationCoordinates,
				ProjectIdentificationNumber = project.ProjectIdentificationNumber,
				ReferencedBy = project.ReferencedBy,
				SalesPerson = project.SalesPerson,
				StartDate = project.StartDate,
				Status = project.Status,
				SystemWarrantyPeriod = project.SystemWarrantyPeriod
			};

			return projectDTO;
		}

		public async Task<GetProjectDTO> UpdateAsync( int userId, int id, AddProjectDTO project ) {
			Int32 companyId = await ServiceUtilities.GetCompanyId( _logger, _repositoryParticipant, _repositorySystemUser, userId );


            Project projectToUpdate = new Project {
				Id = id,
				CompanyId = companyId,
				Address = project.Address,
				Comments = project.Comments,
				Description = project.Description,
				LastUpdatedBy = userId,
				LastUpdatedDateTime = null,
				CommissionDate = project.CommissionDate,
				CoordinatorId = project.CoordinatorId,
				CustomerId = project.CustomerId,
				ElectricityAccountNumber = project.ElectricityAccountNumber,
				ElectricityBoardArea = project.ElectricityBoardArea,
				ElectricityTariffStructure = project.ElectricityTariffStructure,
				EstimatedCost = project.EstimatedCost,
				LocationCoordinates = project.LocationCoordinates,
				ProjectIdentificationNumber = project.ProjectIdentificationNumber,
				ReferencedBy = project.ReferencedBy,
				SalesPerson = project.SalesPerson,
				StartDate = project.StartDate,
				Status = project.Status,
				SystemWarrantyPeriod = project.SystemWarrantyPeriod
			};

			Project? projectUpdated = null;

			try {
				projectUpdated = await _repositoryProject.UpdateAsync( projectToUpdate );
			}
			catch ( Exception ex ) {
				_logger.LogError( ex, ex.Message );
			}

			if ( projectUpdated == null ) {
				throw new Exception( "Project not updated" );
			}

			GetProjectDTO projectDTO = new GetProjectDTO {
				Id = projectUpdated.Id,
				Address = projectUpdated.Address,
				Comments = projectUpdated.Comments,
				Description = projectUpdated.Description,
				CommissionDate = projectUpdated.CommissionDate,
				CoordinatorId = projectUpdated.CoordinatorId,
				CustomerId = projectUpdated.CustomerId,
				ElectricityAccountNumber = projectUpdated.ElectricityAccountNumber,
				ElectricityBoardArea = projectUpdated.ElectricityBoardArea,
				ElectricityTariffStructure = projectUpdated.ElectricityTariffStructure,
				EstimatedCost = projectUpdated.EstimatedCost,
				LocationCoordinates = projectUpdated.LocationCoordinates,
				ProjectIdentificationNumber = projectUpdated.ProjectIdentificationNumber,
				ReferencedBy = projectUpdated.ReferencedBy,
				SalesPerson = projectUpdated.SalesPerson,
				StartDate = projectUpdated.StartDate,
				Status = projectUpdated.Status,
				SystemWarrantyPeriod = projectUpdated.SystemWarrantyPeriod
			};

			return projectDTO;
		}
	}
}
