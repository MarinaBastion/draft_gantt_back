using System.Collections.Generic;
using G=System.Threading.Tasks;
using gantt_backend.Data.Models.Constructor;
using System;
using gantt_backend.Data.ModelsDTO;

namespace gantt_backend.Interfaces.Constructor
{
    public interface ITaskValueRepository: IGenericRepository<TaskValue>
    {   
        Task <List<TaskValueFieldDto>> GetValuesByTask(Guid id);
    }
}