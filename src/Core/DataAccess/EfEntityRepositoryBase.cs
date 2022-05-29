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

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, bool translation = false)
        {
            using (TContext context = new TContext())
            {

                if (translation)
                {
                   /* var properties = typeof(TEntity).GetProperties();
                    var names = properties.Select(p => p.Name).ToArray();
                    var translatedData = from entity in context.Set<TEntity>()
                        from translate in context.Set<Translation>()
                            .Where(t => names.Contains(t.PropertyName)
                                        && t.CultureName.Equals(CurrentUser.User.PreferredLanguage.ToString()) 
                                        && t.Type.Equals(typeof(TEntity).Name) && entity.Id == t.TypeId).DefaultIfEmpty()
                        select new TranslationCollection<TEntity>(entity, translate)
                        {

                        }.Entity;*/
                    //return translatedData.ToList();
                }

                var result = filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
                return result;
            }
        }
    }
}