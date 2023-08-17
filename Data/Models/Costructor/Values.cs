using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gantt_backend.Data.Models.Constructor
{
    public class Value
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Field")]
        public Guid FieldId {get;set;}
        public virtual  Field  Field { get; set; }
        [ForeignKey("Instance")]
        public Guid? InstanceId {get;set;}
        public virtual Instance? Instance { get; set; }
            
        [Column(TypeName = "varchar(1024)")]
        public string? TextData {get;set;}
        [Column(TypeName = "numeric(30, 6)")]
        public double? NumericData {get;set;}
        public bool? BoolData {get;set;}
        public virtual List<Value>? SubValues {get;set;} 
        public virtual Value? ParentValue {get;set;} 
        [ForeignKey("ValueInstance")]
        public Guid? ValueInstanceId {get;set;}
        public virtual Instance? ValueInstance { get; set; }
       
    }
}