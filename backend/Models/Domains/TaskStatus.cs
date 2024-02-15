namespace backend.Models.Domains
{
    public enum TaskStatusCategory
    {
        Active,
        Onhold, 
        WaitingFor,
        Invalid,
        Rejected,
        Done
    }
    public class TaskStatus
    { 
        public Int32? Id { get; set; }
        public required Int32 TaskId { get; set; }
        public required String Status { get; set; }
        public String? Comments { get; set; }
        public Int32? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
    }
}
