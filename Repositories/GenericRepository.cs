using System.Linq.Expressions;
using gantt_backend.Data.DBContext;
using gantt_backend.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace gantt_backend.Repositories
{
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly GanttContext _context;
    public GenericRepository(GanttContext context)
    {
        _context = context;
    }

    public virtual  async Task Add(T entity)
    {
       await _context.Set<T>().AddAsync(entity);
    }

  public virtual async Task<T> Update(T entity)
    {
         _context.Set<T>().Attach(entity);
         _context.Entry(entity).State  = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task BatchUpdate(IEnumerable<T> entity)
    {
         _context.Set<T>().AttachRange(entity);
         _context.Entry(entity).State  = EntityState.Modified;
        await _context.SaveChangesAsync();
       
    }
    public virtual  async Task AddRange(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
    }

    public virtual  async Task< IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().Where(expression).ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

   public virtual  async Task<T> GetById(Guid id)
    {

        return await _context.Set<T>().FindAsync(id);
    }

    public virtual  async Task<T> GetByIds(Guid[] ids)
    {

        return await _context.Set<T>().FindAsync(ids);
    }

    public virtual  async Task Remove(T entity)
    {
         _context.Set<T>().Remove(entity);
    }

    public virtual  async Task Delete(Guid Id)
    {
        throw new NotImplementedException();
    }

    public virtual  async Task RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }
}
}