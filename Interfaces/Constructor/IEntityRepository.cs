using System.Collections.Generic;
using G=System.Threading.Tasks;
using gantt_backend.Data.Models.Constructor;
using System;

namespace gantt_backend.Interfaces.Constructor
{
    public interface IEntityRepository: IGenericRepository<Entity>
    {
        // G.Task<A.Link> Get(Guid id);
        // G.Task<IEnumerable<A.Link>> GetAll();
        // G.Task Add(A.Link task);
        // G.Task Delete(Guid id);      
    }
}