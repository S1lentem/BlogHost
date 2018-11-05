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
        T GetById(int id);
    }
}