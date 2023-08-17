using gantt_backend.Interfaces.Constructor;
using gantt_backend.Data.Models.Constructor;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace gantt_backend.Repositories
{
public class ProjectTypeRepository : GenericRepository<ProjectType>, IProjectTypeRepository
    {
        public ProjectTypeRepository(GanttContext context):base(context)
        {
        }
        public IEnumerable<Entity> GetLAllProjectTypes()
        {
            return _context.Entities.OrderByDescending(d => d.Id).ToList();
        }
         public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.ProjectTypes.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.ProjectTypes.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    

     public override async Task<ProjectType> Update(ProjectType projectType)
        {
            try
            {
                var exist = await _context.ProjectTypes.Where(x => x.Id == projectType.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.CreateDate = DateTime.Now;
                   exist.Decsription = projectType.Decsription;
                   exist.Name = projectType.Name;
               }
                

                return projectType;
            }
            catch (Exception ex)
            {
                return projectType;
            }
        }
}
}