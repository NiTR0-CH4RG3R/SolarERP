using backend.Models.Domains;

namespace backend.Models.DTO.SystemUser {
	public class GetSystemUserDTO : AddSystemUserDTO {
		public Int32? Id { get; set; }
		public Int32? LastUpdatedBy { get; set; }
		public DateTime? LastUpdatedDateTime { get; set; }
	}
}
