using gantt_backend.Interfaces;
using gantt_backend.Interfaces.Utilities;
using A=gantt_backend.Data.Models;
using gantt_backend.Data.Models.Constructor;
using gantt_backend.Data.ViewModels;
using gantt_backend.Data.ModelsDTO;
using AutoMapper;

namespace gantt_backend.Implementation.Utilities
{
    public class FlatMapArray: IDisposable//<T,Y> : IFlatMapArray<T,Y> where T : class where Y : class
    {
        private IMapper _mapper;
         public  FlatMapArray(IMapper mapper)
        {
           
            _mapper = mapper;

        }
        public List<TaskViewModel> ReturnFlatMappedArray(A.Task task,List<TaskViewModel> returnedArr )
        {
            var taskVm = _mapper.Map<TaskViewModel>(task);
            returnedArr.Add(taskVm);

            if (task.SubTasks != null && task.SubTasks.Count() != 0 )
            {
                foreach ( A.Task subtask in task.SubTasks)
                {
                    ReturnFlatMappedArray(subtask,returnedArr);
                }
                return returnedArr;
            }

            else{
                return returnedArr;
            }
        }

 public List<ValueDto> ReturnFlatMappedArray(Value value,List<ValueDto> returnedArr )
        {
            var valueDto = _mapper.Map<ValueDto>(value);
            returnedArr.Add(valueDto);

            if (value.SubValues != null && value.SubValues.Count() != 0 )
            {
                foreach ( Value subvalue in value.SubValues)
                {
                    returnedArr.AddRange(ReturnFlatMappedArray(subvalue,returnedArr));
                }
                return returnedArr;
            }

            else{
                return returnedArr;
            }
        }
         public void Dispose() => Console.WriteLine($"");

    }
}