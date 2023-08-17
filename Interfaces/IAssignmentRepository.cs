using System.Collections.Generic;
using G=System.Threading.Tasks;
using gantt_backend.Data.Models;
using System;

namespace gantt_backend.Interfaces
{
    public interface IAssignmentRepository: IGenericRepository<Assignment>
    {
         Task<List<Assignment>?> GetByTaskId(Guid id);
        

    }
}