using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TripleDerby.Core.Interfaces.Repositories
{
    public interface ITripleDerbyRepository : IEFRepository
    {
        Task<int> Count<T>() where T : class;
        Task<IEnumerable<T>> GetRandom<T>(Expression<Func<T, bool>> expression, int count) where T : class; 
        Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include) where T : class;
    }
}
