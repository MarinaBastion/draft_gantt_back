using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using static System.Linq.Queryable;

namespace gantt_backend.Data.Models.Base
{
    public class Base<T> where T: class
    {
        [Key]
        public Guid Id { get; set; }
        public Base<T>? Parent {get;set;}
        public List<Base<T>>? SubItems {get;set;}   
        public T _obj { get; set;}
        
        
    }


public  class Inherired<C,T>  where T :class where C: Base<T>, new() 
    {
         public Expression<Func<Base<T>,Base<T>>>GetSubItems(int maxDepth,int currentDepth = 0)
          { 
            currentDepth++;
            Expression<Func<Base<T>,Base<T>>> result = item => new Base<T>()
            {
                _obj = item._obj,
                Id = item.Id,
                Parent = item.Parent,
                SubItems = currentDepth == maxDepth ? new List<Base<T>>() 
                : item.SubItems.AsQueryable().Select(GetSubItems(maxDepth,currentDepth)).ToList()
            };
            return result;
          }   
        
        // var f = (int x, int y) => x + y; return f; }
    }
}
