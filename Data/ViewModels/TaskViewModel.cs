using System;
using System.Collections.Generic;

namespace gantt_backend.Data.ViewModels
{
    public class TaskViewModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public int duration { get; set; }
        public decimal progress {get;set;}
        public string parent {get;set;}
        public string type {get;set;}
        public string planned_start { get; set; }
        public string planned_end { get; set; }
        public bool open {
            get {return true;}
            set { }
        }
        public List<AssignViewModel>? user {get;set;}   
        public string? holder {get; set;}
        public string? priority{ get; set;}
        public string? project_type_id {get;set;}
        public List<TaskValueView>? values {get;set;}
    }
}