using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gantt_backend.Data.Models.Constructor
{
    public class EntityFields
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Entity")]
        public Guid EntityId {get;set;}
        public virtual  Entity  Entity { get; set; }
        
        [ForeignKey("Field")]
        public Guid FieldId {get;set;}
        public virtual  Field  Field { get; set; }

    }
}