using System.Collections.Generic;
using G=System.Threading.Tasks;
using gantt_backend.Data.Models;
using System;

namespace gantt_backend.Interfaces
{
    public interface IUserRepository: IGenericRepository<ApplicationUser>
    {
        // G.Task<A.Task> Get(Guid id);
        // G.Task<IEnumerable<A.Task>> GetAll();
        // G.Task Add(A.Task task);
        // G.Task Delete(Guid id);  
        

    }
}