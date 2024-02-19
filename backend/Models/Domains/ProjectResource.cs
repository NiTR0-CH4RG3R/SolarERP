using System;

namespace backend.Models.Domains
{
    public enum ProjectResourceCategory
    {
        Image,
        Document,
        Other
    }
    public class ProjectResource
    {
        public Int32? Id { get; set; }
        public required Int32 ProjectId { get; set; }
        public required String Category { get; set; }
        public required String URL { get; set; }
        public String? Comments { get; set; }
        public Int32? LastUpdateBy { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }

    }
}

  
