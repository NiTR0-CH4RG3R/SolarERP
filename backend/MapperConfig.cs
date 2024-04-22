using AutoMapper;

namespace backend {
	public class MapperConfig {
		public static MapperConfiguration Configure() { 
			return new MapperConfiguration( cfg => {
				cfg.CreateMap<Models.Domains.Participant, Models.DTO.Customer.GetCustomerDTO>();
				cfg.CreateMap<Models.DTO.Customer.AddCustomerDTO, Models.Domains.Participant>();

				cfg.CreateMap<Models.Domains.Participant, Models.DTO.SystemUser.GetSystemUserDTO>();
				cfg.CreateMap<Models.DTO.SystemUser.AddSystemUserDTO, Models.Domains.Participant>();

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
		}
	}
}
