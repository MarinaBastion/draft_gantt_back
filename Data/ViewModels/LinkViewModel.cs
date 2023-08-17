using System;
using System.Collections.Generic;

namespace gantt_backend.Data.ViewModels
{
    public class LinkViewModel
    {
        public Guid id { get; set; }
        public string type { get; set; }
        public string? source { get; set; }
        public string? target { get; set; }
    }
}