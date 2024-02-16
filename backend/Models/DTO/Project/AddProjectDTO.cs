namespace backend.Models.DTO.Project {
	public class AddProjectDTO {
		public required Int32 CustomerId { get; set; }
		public required String Description { get; set; }
		public DateTime? StartDate { get; set; }
		public required String Address { get; set; }
		public Int32? CoordinatorId { get; set; }
		public String? SystemWarrantyPeriod { get; set; }
		public required String Status { get; set; }
		public Decimal? EstimatedCost { get; set; }
		public Int32? ReferencedBy { get; set; }
		public String? LocationCoordinates { get; set; }
		public String? ElectricityTariffStructure { get; set; }
		public String? ElectricityAccountNumber { get; set; }
		public String? ElectricityBoardArea { get; set; }
		public DateTime? CommissionDate { get; set; }
		public String? ProjectIdentificationNumber { get; set; }
		public Int32? SalesPerson { get; set; }
		public String? Comments { get; set; }
	}
}
