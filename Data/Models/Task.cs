using System;
using System.ComponentModel.DataAnnotations.Schema;
using gantt_backend.Data.Models.Constructor;

namespace gantt_backend.Data.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? BaseStart { get; set; }
        public DateTime? BaseEnd { get; set; }
        public int Duration { get; set; }
        public decimal Progress { get; set; }
        public Guid? ParentId { get; set; }
        public string Type { get; set; }
         public bool? Open {
            get {return true;}
            set { }
        }
        public string? Holder {get; set;}
        public string? Priority{ get; set;}
        public virtual Task? ParentTask {get;set;}
        public virtual List<Task>? SubTasks {get;set;}        
        public virtual  List<Assignment>  Resources { get; set; }
        [ForeignKey("ProjectType")]
        public Guid? ProjectTypeId {get;set;}
        public virtual ProjectType? ProjectType {get; set;}
    }
}