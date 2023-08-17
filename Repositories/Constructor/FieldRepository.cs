using gantt_backend.Interfaces.Constructor;
using gantt_backend.Data.Models.Constructor;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace gantt_backend.Repositories
{
public class FieldRepository : GenericRepository<Field>, IFieldRepository
    {
        public FieldRepository(GanttContext context):base(context)
        {
        }
        public IEnumerable<Field> GetLAllFields()
        {
            return _context.Fields.OrderByDescending(d => d.Id).ToList();
        }

         public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.Fields.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.Fields.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    
        public IQueryable<Field> GetAllFieldsByEntityId (Guid id)
        {

            var fields = from e in _context.EntityFields.Where(c => c.EntityId == id)
            join f in _context.Fields on e.FieldId equals f.Id
            select new Field { Id = f.Id, Decsription = f.Decsription, Name = f.Name, CreateDate = f.CreateDate, Type = f.Type };
           
            return  fields.AsQueryable();
        }

        public IQueryable<Field> GetAllFieldsByProjectTypeId (Guid id)
        {
            var fields = from e in _context.ProjectTypeFields.Where(c => c.ProjectTypeId == id)
            join f in _context.Fields on e.FieldId equals f.Id
            select new Field { Id = f.Id, Decsription = f.Decsription, Name = f.Name, CreateDate = f.CreateDate, Type = f.Type };
           
            return  fields.AsQueryable();
        }
        
        public IQueryable<Field> GetAllFieldsByIds(Guid[] ids)
        {
            return _context.Fields.Where(x => ids.Contains(x.Id)).AsQueryable();
        }
     public override async Task<Field> Update(Field field)
        {
            try
            {
                var exist = await _context.Fields.Where(x => x.Id == field.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.CreateDate = DateTime.Now;
                   exist.Decsription = field.Decsription;
                   exist.Name = field.Name;
                   exist.Type = field.Type;
               }
                

                return field;
            }
            catch (Exception ex)
            {
                return field;
            }
        }

        public IQueryable<Field> GetAllFieldsByTaskId(Guid id)
        {
            var fields = from e in _context.ProjectTypeFields.Where(c => c.ProjectTypeId == id)
            join f in _context.Fields on e.FieldId equals f.Id
            select new Field { Id = f.Id, Decsription = f.Decsription, Name = f.Name, CreateDate = f.CreateDate, Type = f.Type };
           
            return  fields.AsQueryable();
        }
    }
}