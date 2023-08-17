using gantt_backend.Interfaces;
using gantt_backend.Data.Models;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace gantt_backend.Repositories
{
public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(GanttContext context):base(context)
        {
        }
        public IEnumerable<Assignment> GetLAllLinks()
        {
            return _context.Assignments.OrderByDescending(d => d.Id).ToList();
        }
         public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.Assignments.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.Assignments.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    
    public async Task<List<Assignment>?> GetByTaskId(Guid id)
    {
         try
            {
                var exist = await _context.Assignments.Where(x => x.TaskId == id).ToListAsync();
                    

               if (exist != null)
                return exist;
            }
            catch (Exception ex)
            {
                return null;
            }
        return null;
    }
     public override async Task<Assignment> Update(Assignment assignment)
        {
            try
            {
                var exist = await _context.Assignments.Where(x => x.Id == assignment.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.Delay = assignment.Delay;
                   exist.Duration = assignment.Duration;
                   exist.EndDate = assignment.EndDate;
                   exist.UserId = assignment.UserId;
                   exist.Value = assignment.Value;

                   
               }
                

                return assignment;
            }
            catch (Exception ex)
            {
                return assignment;
            }
        }
}
}