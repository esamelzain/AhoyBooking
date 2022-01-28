using AhoyBooking.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AhoyBooking.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }
        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.FirstOrDefault();
        }
        public TEntity Get(int id) => Context.Set<TEntity>().Find(id);
        public IEnumerable<TEntity> GetAll(int page, int pageCount)
        {
            pageCount = pageCount == 0 ? 10 : pageCount;
            int skip = pageCount * page;
            return Context.Set<TEntity>().Skip(skip).Take(pageCount).ToList();
        }
        public IEnumerable<TEntity> GetAllIncludes(string[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query;
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => Context.Set<TEntity>().Where(predicate);
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate) => Context.Set<TEntity>().SingleOrDefault(predicate);
        public void Add(TEntity entity) => Context.Set<TEntity>().Add(entity);
        public TEntity AddWithReturned(TEntity entity)
        {
            var added = Context.Add(entity);
            Context.SaveChanges();
            return entity;//(int)entity.GetType().GetProperty("Id").GetValue(entity, null);
        }
        public void AddRange(IEnumerable<TEntity> entities) => Context.Set<TEntity>().AddRange(entities);
        public void Remove(TEntity entity) => Context.Set<TEntity>().Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => Context.Set<TEntity>().RemoveRange(entities);
        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                Context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

            }
        }
        public int SaveChanges() => Context.SaveChanges();
    }
}