using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gantt_backend.Data.ModelsDTO
{
    public class ProjectTypeDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string create_date { get; set; }
        public string? decsription { get; set; }
    }
}