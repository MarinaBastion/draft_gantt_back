using System;

namespace gantt_backend.Data.Models
{
    public class Link
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Guid? SourceTaskId { get; set; }
        public Guid? TargetTaskId { get; set; }
        public virtual Task SourceTask {get;set;}
        public virtual Task TargetTask {get;set;}
        
    }
}