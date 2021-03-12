using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripleDerby.Core.Interfaces.Repositories;

namespace TripleDerby.Infrastructure.Data.Repositories
{
    public abstract class EFRepository : IEFRepository
    {
        private readonly DbContext _context;

        protected EFRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<T> Get<T>(ISpecification<T> spec) where T : class
        {
            var specificationResult = ApplySpecification(spec);

            return await specificationResult.SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> List<T>(ISpecification<T> spec) where T : class
        {
            var specificationResult = ApplySpecification(spec);

            return await specificationResult.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.ToListAsync();
        }

        public async Task<int> Count<T>() where T : class
        {
            var dbSet = _context.Set<T>();

            return await dbSet.CountAsync();
        }

        public T Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);

            return entity;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : class
        {
            var evaluator = new SpecificationEvaluator();

            return evaluator.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
