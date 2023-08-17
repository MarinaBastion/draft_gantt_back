using System.Collections.Generic;
using G=System.Threading.Tasks;
using gantt_backend.Data.Models.Constructor;
using System;

namespace gantt_backend.Interfaces.Constructor
{
    public interface IFieldRepository: IGenericRepository<Field>
    {   
        IQueryable<Field> GetAllFieldsByIds(Guid[] ids);
        IQueryable<Field> GetAllFieldsByEntityId (Guid id);
        IQueryable<Field> GetAllFieldsByProjectTypeId (Guid id);
        IQueryable<Field> GetAllFieldsByTaskId (Guid id);
    }
}