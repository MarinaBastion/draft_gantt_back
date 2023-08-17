using gantt_backend.Interfaces;
using gantt_backend.Data.Models;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace gantt_backend.Repositories
{
public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(GanttContext context):base(context)
        {
        }
        public IEnumerable<ApplicationUser> GetLAllUsers()
        {
            return _context.ApplicationUsers.OrderByDescending(d => d.Id).ToList();
        }
         public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.ApplicationUsers.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.ApplicationUsers.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    

     public override async Task<ApplicationUser> Update(ApplicationUser user)
        {
            try
            {
                var exist = await _context.ApplicationUsers.Where(x => x.Id == user.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.FullName = user.FullName;
               }
                

                return user;
            }
            catch (Exception ex)
            {
                return user;
            }
        }
}
}