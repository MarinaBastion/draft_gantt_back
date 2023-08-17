using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using gantt_backend.Data.Models.Base;
using gantt_backend.Data.Models;
using System.Linq.Expressions;

namespace gantt_backend.Data.Models.Constructor
{
    public class InstanceValue : Base<InstanceValue>
     {
    //      public Expression<Func<InstanceValue,InstanceValue>>GetSubItems(int maxDepth,int currentDepth = 0)
    //       { 
    //         currentDepth++;
    //         Expression<Func<InstanceValue,InstanceValue>> result = item => new InstanceValue()
    //         {
    //             _obj = item._obj,
    //             Id = item.Id,
    //             Parent = item.Parent,
    //             SubItems = currentDepth == maxDepth ? new List<InstanceValue>() 
    //             : item.SubItems.AsQueryable().OfType<x.Select(GetSubItems(maxDepth,currentDepth)).ToList()
    //         };
    //         return result;
    //       }   
        
    }
}