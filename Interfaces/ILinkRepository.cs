using System.Collections.Generic;
using G=System.Threading.Tasks;
using A= gantt_backend.Data.Models;
using System;

namespace gantt_backend.Interfaces
{
    public interface ILinkRepository: IGenericRepository<A.Link>
    {
        // G.Task<A.Link> Get(Guid id);
        // G.Task<IEnumerable<A.Link>> GetAll();
        // G.Task Add(A.Link task);
        // G.Task Delete(Guid id);      
    }
}