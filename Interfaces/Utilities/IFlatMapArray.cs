using System.Collections.Generic;
using G=System.Threading.Tasks;
using gantt_backend.Data.Models;
using System;

namespace gantt_backend.Interfaces.Utilities
{
    public interface IFlatMapArray<T,Y> where T : class where Y : class
    {
        List<Y> ReturnFlatMappedArray(T task,List<Y> returnedArr);
        // Y ReturnFlatMappedArray(T task, Y returnedArr);
    }
}