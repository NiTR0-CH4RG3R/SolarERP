namespace backend.Models.Domains
{
    public enum TaskResourceCategory
    {
        Image,
        Document,
        Other
    }
    public class TaskResource
    {
        public required Int32 TaskId { get; set; }
        public required String URL { get; set; }
        public required String Category { get; set; }
        public String? Comments { get; set; }
        public Int32? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
    }
}
