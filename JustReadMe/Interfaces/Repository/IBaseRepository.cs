using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JustReadMe.Interfaces
{
    public interface IBaseRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T item);
        void Remove(T item);
        void Update(T item);
    }
}