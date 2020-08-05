using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TripleDerby.Core.Interfaces.Repositories;

namespace TripleDerby.Infrastructure.Data.Repositories
{
    public class TripleDerbyRepository : EFRepository, ITripleDerbyRepository
    {
        private readonly TripleDerbyContext _context;

        public TripleDerbyRepository(TripleDerbyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> Count<T>() where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.CountAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include) where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.Include(include).Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetRandom<T>(Expression<Func<T, bool>> expression, int count) where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.Where(expression).OrderBy(x => Guid.NewGuid()).Take(count).ToListAsync();
        }
    }
}
