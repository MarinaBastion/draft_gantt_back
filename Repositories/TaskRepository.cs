using gantt_backend.Interfaces;
using A=gantt_backend.Data.Models;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using G=System.Threading.Tasks;

namespace gantt_backend.Repositories
{
public class TaskRepository : GenericRepository<A.Task>, ITaskRepository
    {
        public TaskRepository(GanttContext context):base(context)
        {
        }
        public IQueryable<A.Task> GetAllTasks()
        {
            return _context.Tasks.OrderByDescending(d => d.Id).AsQueryable();
        }

 
        public async G.Task<List<A.Task>> GetTasksByProjectId(Guid id)
        {
               var result =  _context.Tasks.Include(c => c.Resources).Where(x => ((x.ParentId == null) && (x.Id == id)))
               .Select(GetSubTasks(7,0));
               //var subtasks = result.Where(x => ((x.ParentId == null) && (x.Id == id))).AsQueryable();
               return await result.ToListAsync();
        }
        public IQueryable<A.Task> GetOnlyProjects(){
            return _context.Tasks.Where(d => (d.Type == "project")).AsQueryable();
        }
          public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.Tasks.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.Tasks.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override async Task<A.Task> Update(A.Task task)
        {
            try
            {
                var exist = await _context.Tasks.Where(x => x.Id == task.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.Duration = task.Duration;
                   exist.EndDate = task.EndDate;
                   exist.ParentId = task.ParentId;
                   exist.Priority = task.Priority;
                   exist.Progress = task.Progress;
                   exist.StartDate = task.StartDate;
                   exist.Type = task.Type;
                   exist.Text = task.Text;
                   exist.BaseStart = task.BaseStart;
                   exist.BaseEnd = task.BaseEnd;
                   exist.ProjectTypeId = task.ProjectTypeId;
                   exist.ProjectType = task.ProjectType;

               }
                

                return task;
            }
            catch (Exception ex)
            {
                return task;
            }
        }

        public static Expression<Func<A.Task,A.Task>>GetSubTasks(int maxDepth,int currentDepth = 0)
          { 
            currentDepth++;
            Expression<Func<A.Task,A.Task>> result = task => new A.Task()
            {
                Id = task.Id,
                Duration = task.Duration,
                EndDate = task.EndDate,
                StartDate = task.StartDate,
                Text = task.Text,
                Progress = task.Progress,
                ParentId = task.ParentId,
                Priority = task.Priority,
                Open = task.Open,
                Holder = task.Holder,
                Type = task.Type,
                BaseStart = task.BaseStart,
                BaseEnd = task.BaseEnd,
                Resources = task.Resources,
                SubTasks = currentDepth == maxDepth ? new List<A.Task>() 
                : task.SubTasks.AsQueryable().Select(GetSubTasks(maxDepth,currentDepth)).ToList()
            };
            return result;
          }

    }
}