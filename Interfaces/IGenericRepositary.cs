using System.Linq.Expressions;
using System.Threading.Tasks;
namespace gantt_backend.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<T> GetByIds(Guid[] ids);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        Task<T> Update(T entity);
        Task BatchUpdate(IEnumerable<T> entity);
        Task AddRange(IEnumerable<T> entities);
        Task Remove(T entity);
        Task Delete(Guid Id);
        Task RemoveRange(IEnumerable<T> entities);
    }
}