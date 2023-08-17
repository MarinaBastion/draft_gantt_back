using System.Collections.Generic;
using G=System.Threading.Tasks;
using gantt_backend.Data.Models.Constructor;
using System;

namespace gantt_backend.Interfaces.Constructor
{
    public interface IInstanceRepository: IGenericRepository<Instance>
    {
       IQueryable<Instance> GetAllInstancesByEntityId (Guid id);
    }
}