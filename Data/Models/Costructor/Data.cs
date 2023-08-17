using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gantt_backend.Data.Models.Constructor
{
    public class Data
    {
        [ForeignKey("Field")]
        public Guid FieldId {get;set;}
        public virtual  Field  Field { get; set; }
        [ForeignKey("Instance")]
        public Guid InstanceId {get;set;}
        public virtual Instance Instance { get; set; }
        [Column(TypeName = "varchar(1024)")]
        public string? TextData {get;set;}
        [Column(TypeName = "numeric(30, 6)")]
        public double? NumericData {get;set;}
        public bool? BoolData {get;set;}
       
    }
}