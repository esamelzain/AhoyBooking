using AhoyBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AhoyBooking.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        TEntity Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
        IEnumerable<TEntity> GetAll(int page,int pageCount);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAllIncludes(string[] includes);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        TEntity AddWithReturned(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        int SaveChanges();
    }
}