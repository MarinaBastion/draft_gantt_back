using Microsoft.EntityFrameworkCore;
using gantt_backend.Data.Models;
using gantt_backend.Data.Models.Constructor;
using A=gantt_backend.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace gantt_backend.Data.DBContext
{
 public class GanttContext : IdentityDbContext<ApplicationUser,  ApplicationRole ,Guid>
    {
        public GanttContext(DbContextOptions<GanttContext> options)
           : base(options)
        {
            
        }
        public DbSet<A.Task> Tasks { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set;}
        public DbSet<ApplicationRole> ApplicationRoles { get; set;}
        public DbSet<Assignment> Assignments { get; set;}
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Field> Fields  { get; set; }
        public DbSet<EntityFields> EntityFields { get; set; }
        public DbSet<Instance> Instances { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<TaskValue> TaskValues { get; set; }
        public DbSet<ProjectTypeFields> ProjectTypeFields { get; set; }
 
    }
    }