﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace TripleDerby.Core.Interfaces.Repositories
{
    public interface IEFRepository
    {
        Task<T> Get<T>(ISpecification<T> spec) where T : class;
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task<int> Count<T>() where T : class;
        T Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IReadOnlyList<T>> List<T>(ISpecification<T> spec) where T : class;
        Task Save();
    }
}
