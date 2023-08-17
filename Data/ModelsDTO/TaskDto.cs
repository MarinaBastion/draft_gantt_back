using System;
using System.Collections.Generic;

namespace gantt_backend.Data.ModelsDTO
{
    public class TaskDto
    {
        public int duration { get; set; }
        public string end_date { get; set; }
        public Guid id { get; set; }
        public Guid? parent {get;set;}
        public decimal progress {get;set;}
        public string start_date { get; set; }
         public string planned_start { get; set; }
        public string planned_end { get; set; }
        public string text { get; set; }
        public string? type { get; set; }
        public List<AssignmentDto>? user {get;set;} 
        public string? project_type_id {get;set;}
    }
}
