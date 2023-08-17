using gantt_backend.Interfaces.Constructor;
using gantt_backend.Data.Models.Constructor;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace gantt_backend.Repositories
{
public class ProjectTypeFieldsRepository : GenericRepository<ProjectTypeFields>, IProjectTypeFieldsRepository
    {
        public ProjectTypeFieldsRepository(GanttContext context):base(context)
        {
        }
        public IEnumerable<ProjectTypeFields> GetLAllEntityFields()
        {
            return _context.ProjectTypeFields.OrderByDescending(d => d.Id).ToList();
        }
         public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.ProjectTypeFields.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.ProjectTypeFields.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    

     public override async Task<ProjectTypeFields> Update(ProjectTypeFields projectTypeField)
        {
            try
            {
                var exist = await _context.ProjectTypeFields.Where(x => x.Id == projectTypeField.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.ProjectTypeId = projectTypeField.ProjectTypeId;
                   exist.FieldId = projectTypeField.FieldId;
               }
                

                return projectTypeField;
            }
            catch (Exception ex)
            {
                return projectTypeField;
            }
        }
}
}