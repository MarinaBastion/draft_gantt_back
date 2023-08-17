using System.Collections.Generic;
using G=System.Threading.Tasks;
using A=gantt_backend.Data.Models;
using System;

namespace gantt_backend.Interfaces
{
    public interface ITaskRepository: IGenericRepository<A.Task>
    {
        // G.Task<A.Task> Get(Guid id);
        // G.Task<IEnumerable<A.Task>> GetAll();
        // G.Task Add(A.Task task);
        // G.Task Delete(Guid id);  
       IQueryable<A.Task> GetAllTasks();
       IQueryable<A.Task> GetOnlyProjects();

       G.Task<List<A.Task>> GetTasksByProjectId(Guid id);
    }
}