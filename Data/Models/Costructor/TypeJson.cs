using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace gantt_backend.Data.Models.Constructor
{
    public class TypeJson
    {
        public string SimpleType { get; set; }
        public string DirectoryId { get; set; }
        public string InstanceDirectoryId { get; set; }
        // TODO: delete
        public string TaskId { get; set; }
    }
}