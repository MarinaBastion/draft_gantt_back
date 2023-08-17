using gantt_backend.Interfaces.Constructor;
using gantt_backend.Data.Models.Constructor;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace gantt_backend.Repositories
{
public class InstanceRepository : GenericRepository<Instance>, IInstanceRepository
    {
        public InstanceRepository(GanttContext context):base(context)
        {
        }
        public IEnumerable<Instance> GetLAllInstances()
        {
            return _context.Instances.OrderByDescending(d => d.Id).ToList();
        }
         public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.Instances.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.Instances.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    
        public IQueryable<Instance> GetAllInstancesByEntityId (Guid id)
        {
            var i = _context.Instances.Where(c => c.EntityId == id);
            return i.AsQueryable();
        }

        
        public override async Task<Instance> Update(Instance instance)
        {
            try
            {
                var exist = await _context.Instances.Where(x => x.Id == instance.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.CreateDate = DateTime.Now;
                   exist.Decsription = instance.Decsription;
                   exist.Name = instance.Name;
                   exist.EntityId = instance.EntityId;
               }
                

                return instance;
            }
            catch (Exception ex)
            {
                return instance;
            }
        }
}
}