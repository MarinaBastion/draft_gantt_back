using System;
using System.Collections.Generic;

namespace gantt_backend.Data.ModelsDTO
{
    public class LinkDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Guid? SourceTaskId { get; set; }
        public Guid? TargetTaskId { get; set; }
    }
}