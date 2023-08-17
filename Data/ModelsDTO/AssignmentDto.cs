using System;
namespace gantt_backend.Data.ModelsDTO
{
    public class AssignmentDto
    {
        public string id { get; set; }
        public string start_date { get; set; }
        public int? duration { get; set; }
        public string? end_date { get; set; }
        public int? delay { get; set; }
        public string resource_id { get; set; }
        public string? task_id { get; set; }
        public string? mode {get;set;}
        public string value {get;set;}
    }
}
