using System;
using System.Collections.Generic;

namespace gantt_backend.Data.ModelsDTO
{
    public class EntityFieldDto
    {
        public Guid id { get; set; }
        public Guid entity_id {get;set;}
        public Guid field_id {get;set;}
    }
}
