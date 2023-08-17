using gantt_backend.Interfaces.Constructor;
using gantt_backend.Data.Models.Constructor;
using gantt_backend.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using gantt_backend.Implementation.Utilities;
using AutoMapper;
using gantt_backend.Data.ModelsDTO;
using gantt_backend.Data.Models.Base;
using A=gantt_backend.Data.Models.Constructor;
using System.Linq.Expressions;

namespace gantt_backend.Repositories
{
public class ValueRepository : GenericRepository<Value>, IValueRepository
    {
        private IMapper _mapper;
        public ValueRepository(GanttContext context, IMapper mapper):base(context)
        {
            _mapper = mapper;
        }
        public IEnumerable<Value> GetLAllInstances()
        {
            return _context.Values.OrderByDescending(d => d.Id).ToList();
        }
         public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.Values.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.Values.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    
    public async Task<List<Value>?> GetFirstLevelValue(Guid id)
        {
           var result =  _context.Values.Where(x => ((x.ParentValue.Id == null) && (x.Id == id)))
               .Select(GetSubValues(20,0));
            
             return await result.ToListAsync();
        }
     public override async Task<Value> Update(Value value)
        {
            try
            {
                var exist = await _context.Values.Where(x => x.Id == value.Id)
                                        .FirstOrDefaultAsync();

               if (exist != null)
               {
                   exist.FieldId = value.FieldId;
                   exist.InstanceId = value.InstanceId;
                   exist.BoolData = value.BoolData;
                   exist.NumericData = value.NumericData;
                   exist.ParentValue = value.ParentValue;
                   exist.TextData = value.TextData;
                   exist.ValueInstanceId = value.InstanceId;
               }
                

                return value;
            }
            catch (Exception ex)
            {
                return value;
            }
        }

         public static Expression<Func<Value,Value>>GetSubValues(int maxDepth,int currentDepth = 0)
          { 
            currentDepth++;
            Expression<Func<Value,Value>> result = value => new Value()
            {
                Id = value.Id,
                ParentValue = value.ParentValue,
                Field = value.Field,
                FieldId = value.FieldId,
                Instance = value.Instance,
                InstanceId = value.InstanceId,
                TextData = value.TextData,
                NumericData = value.NumericData,
                BoolData = value.BoolData,
                ValueInstanceId = value.ValueInstanceId,
                SubValues = currentDepth == maxDepth ? new List<Value>() 
                : value.SubValues.AsQueryable().Select(GetSubValues(maxDepth,currentDepth)).ToList()
            };
            return result;
          }

           public async Task <List<Value>> GetValuesByInstance(Guid id){
                var result =  _context.Values.AsQueryable().Where(x => x.InstanceId == id);
                return await result.ToListAsync();
           }
           
}
}