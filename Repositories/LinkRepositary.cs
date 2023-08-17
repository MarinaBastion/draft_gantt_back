using gantt_backend.Interfaces;
using gantt_backend.Data.Models;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace gantt_backend.Repositories
{
public class LinkRepository : GenericRepository<Link>, ILinkRepository
    {
        public LinkRepository(GanttContext context):base(context)
        {
        }
        public IEnumerable<Link> GetLAllLinks()
        {
            return _context.Links.OrderByDescending(d => d.Id).ToList();
        }
         public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.Links.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.Links.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    

     public override async Task<Link> Update(Link link)
        {
            try
            {
                var exist = await _context.Links.Where(x => x.Id == link.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.SourceTaskId = link.SourceTaskId;
                   exist.TargetTaskId = link.TargetTaskId;
               }
                

                return link;
            }
            catch (Exception ex)
            {
                return link;
            }
        }
}
}