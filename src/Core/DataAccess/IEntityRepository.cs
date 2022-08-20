using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entity.Abstracts;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        T Get(Expression<Func<T, bool>> filter);

        List<T> GetAll(Expression<Func<T, bool>> filter = null, bool lazyLoading = false, params Expression<Func<T, object>>[] including);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task<T> GetAsync(Expression<Func<T, bool>> filter);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, bool translation = false, params Expression<Func<T, object>>[] including);
    }
}