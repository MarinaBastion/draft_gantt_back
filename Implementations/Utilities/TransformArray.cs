using gantt_backend.Interfaces;
using gantt_backend.Interfaces.Utilities;
using A=gantt_backend.Data.Models;
using gantt_backend.Data.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using gantt_backend.Data.Models.Base;

namespace gantt_backend.Implementation.Utilities
{
    public class TransformArray<T,Y,C>  : IDisposable where C :class  where T :Base<C> ,new() where Y : class 
    {
        // private IMapper _mapper;
        //  public  TransformArray(IMapper mapper)
        // {
           
        //     _mapper = mapper;

        // }
        // public List<Y> ReturnFlatMappedArray(T task,List<Y> returnedArr )
        // {
        //     var taskVm = _mapper.Map<Y>(task);
        //     returnedArr.Add(taskVm);

        //     if (task.SubItems != null && task.SubItems.Count() != 0 )
        //     {
        //         foreach ( T subtask in task.SubItems)
        //         {
        //             returnedArr.AddRange(ReturnFlatMappedArray(subtask,returnedArr));
        //         }
        //         return returnedArr;
        //     }

        //     else{
        //         return returnedArr;
        //     }
        // }

        // public Expression<Func<T,T>>GetSubItems(int maxDepth,int currentDepth = 0)
        //   { 
        //     currentDepth++;
        //     Expression<Func<T,T>> result = item => new T()
        //     {
        //         _obj = item._obj,
        //         Id = item.Id,
        //         Parent = item.Parent,
        //         SubItems = currentDepth == maxDepth ? new List<T>() 
        //         : item.SubItems!.AsQueryable().Select(GetSubItems(maxDepth,currentDepth)).ToList()
        //     };
        //     return result;
        //   }   

      

         public void Dispose() => Console.WriteLine($"");

    }
}

