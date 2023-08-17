using System;
using System.Collections.Generic;

namespace gantt_backend.Data.ViewModels
{
    public class UserViewModel
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public string? parent { get; set; }
        public string? unit { get; set; }
    }
}