using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Core.Entity.Abstracts;
using Core.Entity.Concretes;
using Core.Utilities.Language;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Core.DataAccess
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public TEntity Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return addedEntity.Entity;
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {

                var result = context.Set<TEntity>().SingleOrDefault(filter);
                return result;
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, bool translation = false, params Expression<Func<TEntity, object>>[] including)
        {
            using (TContext context = new TContext())
            {
                if (translation)
                {
                    var include = context.Set<TEntity>().AsQueryable();
                    including.ToList().ForEach(item =>
                    {
                        include = include.Include(item);
                    });
                    return include.ToList();
                }
                else
                {
                    var result = filter == null
                        ? context.Set<TEntity>().ToList()
                        : context.Set<TEntity>().Where(filter).ToList();
                    return result;

                }
            }

        }
    }
}