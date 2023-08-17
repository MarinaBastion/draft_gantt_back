using System.Collections.Generic;
using G=System.Threading.Tasks;
using gantt_backend.Data.Models.Constructor;
using System;

namespace gantt_backend.Interfaces.Constructor
{
    public interface IValueRepository: IGenericRepository<Value>
    {   
        Task<List<Value>> GetFirstLevelValue(Guid id);
        Task <List<Value>> GetValuesByInstance(Guid id);
    }
}