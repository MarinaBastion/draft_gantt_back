using System;
using System.Collections.Generic;

namespace gantt_backend.Data.ModelsDTO
{
    public class ProjectTypeFieldsDto
    {
        public Guid id { get; set; }
        public Guid project_type_id {get;set;}
        public Guid field_id {get;set;}
    }
}
