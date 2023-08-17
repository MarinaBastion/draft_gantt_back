using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace gantt_backend.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public virtual Guid? ParentId {get; set;}
        

    }
}