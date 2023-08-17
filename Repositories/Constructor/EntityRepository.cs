using gantt_backend.Interfaces.Constructor;
using gantt_backend.Data.Models.Constructor;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace gantt_backend.Repositories
{
public class EntityRepository : GenericRepository<Entity>, IEntityRepository
    {
        public EntityRepository(GanttContext context):base(context)
        {
        }
        public IEnumerable<Entity> GetLAllEntities()
        {
            return _context.Entities.OrderByDescending(d => d.Id).ToList();
        }
         public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.Entities.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.Entities.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    

     public override async Task<Entity> Update(Entity entity)
        {
            try
            {
                var exist = await _context.Entities.Where(x => x.Id == entity.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.CreateDate = DateTime.Now;
                   exist.Decsription = entity.Decsription;
                   exist.Name = entity.Name;
               }
                

                return entity;
            }
            catch (Exception ex)
            {
                return entity;
            }
        }
}
}