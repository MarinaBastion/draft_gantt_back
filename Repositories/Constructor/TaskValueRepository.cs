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
public class TaskValueRepository : GenericRepository<TaskValue>, ITaskValueRepository
    {
        private IMapper _mapper;
        public TaskValueRepository(GanttContext context, IMapper mapper):base(context)
        {
            _mapper = mapper;
        }
        public IEnumerable<TaskValue> GetLAllInstances()
        {
            return _context.TaskValues.OrderByDescending(d => d.Id).ToList();
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await _context.TaskValues.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                _context.TaskValues.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    
    
     public override async Task<TaskValue> Update(TaskValue value)
        {
            try
            {
                var exist = await _context.TaskValues.Where(x => x.Id == value.Id)
                                        .FirstOrDefaultAsync();
                if (exist != null)
                {
                    exist.FieldId = value.FieldId;
                    exist.TaskId = value.TaskId;
                    exist.BoolData = value.BoolData;
                    exist.NumericData = value.NumericData;
                    exist.TextData = value.TextData;
                    exist.InstanceId = value.InstanceId;
                }
                return value;
            }
            catch (Exception ex)
            {
                return value;
            }
        }
        public async Task<List<TaskValueFieldDto>?> GetValuesByTask(Guid id)
        {
             var values = await _context.TaskValues.Where(x => x.TaskId == id).ToListAsync();
             var fieldsRes = await _context.Fields.ToListAsync();

             if (values.Count != 0)
             {
                List<TaskValueFieldDto> fields = fieldsRes.Join(values, c => c.Id, ct => ct.FieldId, (c,ct) => new TaskValueFieldDto{
                id = ct.Id,
                field_id = ct.FieldId,
                task_id = ct.TaskId,
                text_data = ct.TextData,
                numeric_data = ct.NumericData,
                bool_data = ct.BoolData,
                instance_id = ct.InstanceId,
                field = c.Name,
                value_id = ct.ValueId,
                type =  new TypeJsonDto { simple_type = c.Type.SimpleType, directory_id = c.Type.DirectoryId, instance_directory_id = c.Type.InstanceDirectoryId}
                }).ToList();
                return fields; 
             }    
            else    
                return null;    
        }
    }
}