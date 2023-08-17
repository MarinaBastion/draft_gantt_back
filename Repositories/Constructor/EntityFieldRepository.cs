using gantt_backend.Interfaces.Constructor;
using gantt_backend.Data.Models.Constructor;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace gantt_backend.Repositories
{
public class EntityFieldRepository : GenericRepository<EntityFields>, IEntityFieldRepository
    {
        public EntityFieldRepository(GanttContext context):base(context)
        {
        }
        public IEnumerable<EntityFields> GetLAllEntityFields()
        {
            return _context.EntityFields.OrderByDescending(d => d.Id).ToList();
        }
         public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.EntityFields.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.EntityFields.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    

     public override async Task<EntityFields> Update(EntityFields entityField)
        {
            try
            {
                var exist = await _context.EntityFields.Where(x => x.Id == entityField.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.EntityId = entityField.EntityId;
                   exist.FieldId = entityField.FieldId;
               }
                

                return entityField;
            }
            catch (Exception ex)
            {
                return entityField;
            }
        }
}
}