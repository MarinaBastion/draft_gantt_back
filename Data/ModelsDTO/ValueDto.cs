using System;
using System.Collections.Generic;

namespace gantt_backend.Data.ModelsDTO
{
    public class ValueDto
    {
        public Guid? id { get; set; }
        public Guid field_id {get;set;}
        public Guid? instance_id {get;set;}
        public string? text_data {get;set;}
        public double? numeric_data {get;set;}
        public bool? bool_data {get;set;}
        public Guid? parent_value_id {get;set;} 
        public Guid? value_instance_id {get;set;}
       
    }
}