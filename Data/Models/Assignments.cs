using System;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace gantt_backend.Data.Models
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Value { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Delay {get; set;}
        public int? Duration {get; set;}
        public string? Mode {get;set;}
        public string? Unit {get;set;}
        [ForeignKey("Task")]
        public Guid TaskId {get;set;}
        public virtual Task? Task { get; set; }
    }
}
