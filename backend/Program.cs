
using AutoMapper;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services;
using backend.Services.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace backend {
	public class Program {
		public static void Main( string[] args ) {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Add CORS
			builder.Services.AddCors( options => {
				options.AddDefaultPolicy( policy => {
					policy.WithOrigins( builder.Configuration.GetSection( "CORS" ).GetSection( "AllowedOrigins" ).Get<String[]>()  ).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
				} );
			} );

			// Create the automapper configuration
			var mapperConfig = new MapperConfiguration( cfg => {
				cfg.CreateMap<Models.Domains.Participant, Models.DTO.Customer.GetCustomerDTO>();
				cfg.CreateMap<Models.DTO.Customer.AddCustomerDTO, Models.Domains.Participant>();

				cfg.CreateMap<Models.Domains.SystemUser, Models.DTO.SystemUser.GetSystemUserDTO>();
				cfg.CreateMap<Models.DTO.SystemUser.AddSystemUserDTO, Models.Domains.SystemUser>();

				cfg.CreateMap<Models.Domains.Vendor, Models.DTO.Vendor.GetVendorDTO>();
				cfg.CreateMap<Models.DTO.Vendor.AddVendorDTO, Models.Domains.Vendor>();

				cfg.CreateMap<Models.Domains.VendorItem, Models.DTO.VendorItem.GetVendorItemDTO>();
				cfg.CreateMap<Models.DTO.VendorItem.AddVendorItemDTO, Models.Domains.VendorItem>();

				cfg.CreateMap<Models.Domains.Project, Models.DTO.Project.GetProjectDTO>();
				cfg.CreateMap<Models.DTO.Project.AddProjectDTO, Models.Domains.Project>();

				cfg.CreateMap<Models.Domains.ProjectCommisionReport, Models.DTO.ProjectCommisionReport.GetProjectCommisionReportDTO>();
				cfg.CreateMap<Models.DTO.ProjectCommisionReport.AddProjectCommisionReportDTO, Models.Domains.ProjectCommisionReport>();

				cfg.CreateMap<Models.Domains.ProjectResource, Models.DTO.ProjectResource.GetProjectResourceDTO>();
				cfg.CreateMap<Models.DTO.ProjectResource.AddProjectResourceDTO, Models.Domains.ProjectResource>();

				cfg.CreateMap<Models.Domains.ProjectItem, Models.DTO.ProjectItem.GetProjectItemDTO>();
				cfg.CreateMap<Models.DTO.ProjectItem.AddProjectItemDTO, Models.Domains.ProjectItem>();

				cfg.CreateMap<Models.Domains.ProjectService, Models.DTO.ProjectService.GetProjectServiceDTO>();
				cfg.CreateMap<Models.DTO.ProjectService.AddProjectServiceDTO, Models.Domains.ProjectService>();

				cfg.CreateMap<Models.Domains.ProjectTest, Models.DTO.ProjectTest.GetProjectTestDTO>();
				cfg.CreateMap<Models.DTO.ProjectTest.AddProjectTestDTO, Models.Domains.ProjectTest>();

				cfg.CreateMap<Models.Domains.Task, Models.DTO.Task.GetTaskDTO>();
				cfg.CreateMap<Models.DTO.Task.AddTaskDTO, Models.Domains.Task>();

				cfg.CreateMap<Models.Domains.TaskStatus, Models.DTO.TaskStatus.GetTaskStatusDTO>();
				cfg.CreateMap<Models.DTO.TaskStatus.AddTaskStatusDTO, Models.Domains.TaskStatus>();

				cfg.CreateMap<Models.Domains.TaskResource, Models.DTO.TaskResource.GetTaskResourceDTO>();
				cfg.CreateMap<Models.DTO.TaskResource.AddTaskResourceDTO, Models.Domains.TaskResource>();
			} );

			// Add the automapper
			builder.Services.AddSingleton( mapperConfig.CreateMapper() );

			// Add the database service
			builder.Services.AddScoped<IDbConnection, MySqlConnection>( x => new MySqlConnection( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );

			// Add our repositories
			builder.Services.AddScoped<IRepositoryCompany, RepositoryCompany>();
			builder.Services.AddScoped<IRepositoryParticipant, RepositoryParticipant>();
			builder.Services.AddScoped<IRepositorySystemUser, RepositorySystemUser>();
			builder.Services.AddScoped<IRepositoryVendor, RepositoryVendor>();
			builder.Services.AddScoped<IRepositoryVendorItem, RepositoryVendorItem>();
			builder.Services.AddScoped<IRepositoryTask, RepositoryTask>();
			builder.Services.AddScoped<IRepositoryTaskStatus, RepositoryTaskStatus>();
			builder.Services.AddScoped<IRepositoryTaskResource, RepositoryTaskResource>();
			builder.Services.AddScoped<IRepositoryProject, RepositoryProject>();

			// Add our services
			builder.Services.AddScoped<IServiceAuthentication, ServiceAuthentication>();
			builder.Services.AddScoped<IServiceCustomer, ServiceCustomer>();
			builder.Services.AddScoped<IServiceSystemUser, ServiceSystemUser>();
			builder.Services.AddScoped<IServiceVendor, ServiceVendor>();
			builder.Services.AddScoped<IServiceVendorItem, ServiceVendorItem>();
			builder.Services.AddScoped<IServiceTask, ServiceTask>();
			builder.Services.AddScoped<IServiceTaskStatus, ServiceTaskStatus>();
			builder.Services.AddScoped<IServiceTaskResource, ServiceTaskResource>();
			builder.Services.AddScoped<IServiceProject, ServiceProject>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if ( app.Environment.IsDevelopment() ) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseCors();

			app.MapControllers();

			app.Run();
		}
	}
}
