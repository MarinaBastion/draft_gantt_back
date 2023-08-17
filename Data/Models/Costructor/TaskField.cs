using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gantt_backend.Data.Models.Constructor
{
    public class TaskFields
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ProjectType")]
        public Guid ProjectTypeId {get;set;}
        public virtual ProjectType ProjectType { get; set; }
        
        [ForeignKey("Field")]
        public Guid FieldId {get;set;}
        public virtual  Field  Field { get; set; }

    }
}