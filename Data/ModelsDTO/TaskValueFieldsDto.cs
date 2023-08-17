using System;
using System.Collections.Generic;

namespace gantt_backend.Data.ModelsDTO
{
    public class TaskValueFieldDto
    {
        public Guid? id { get; set; }
        public Guid field_id {get;set;}
        public Guid? task_id {get;set;}
        public string? text_data {get;set;}
        public double? numeric_data {get;set;}
        public bool? bool_data {get;set;}
        public Guid? instance_id {get;set;}
        public Guid? value_id {get;set;}
        public string field {get;set;}
        public TypeJsonDto type { get; set; }
       
    }
}
    