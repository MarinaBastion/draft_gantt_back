using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gantt_backend.Data.Models.Constructor
{
    public class Instance
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(255, ErrorMessage="Наименование не должно быть больше 255 символов")]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        [MaxLength(1000, ErrorMessage="Описание не должно быть больше 1000 символов")]
        public string Decsription { get; set; }
        [ForeignKey("Entity")]
        public Guid EntityId {get;set;}
        public virtual  Entity  Entity { get; set; }
        public virtual List<Instance>? SubInstances {get;set;} 
        public virtual Instance? ParentInstance {get;set;} 

       
    }
}