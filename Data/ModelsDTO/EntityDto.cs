using System;
using System.Collections.Generic;

namespace gantt_backend.Data.ModelsDTO
{
    public class EntityDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string create_date { get; set; }
        public string decsription { get; set; }
    }
}