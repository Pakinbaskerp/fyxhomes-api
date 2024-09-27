// using Contract.IRepository;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using API.Contract.IRepository;
using API.Data;

namespace API
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        protected AppDbContext RepositoryContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryContext"></param>
        protected RepositoryBase(AppDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTracking();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public int FindCountByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T FindFirstByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Create(T entity)
        {
            _ = RepositoryContext.Set<T>().Add(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void CreateBatch(List<T> entities)
        {
            RepositoryContext.Set<T>().AddRange(entities);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _ = RepositoryContext.Set<T>().Update(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateBatch(List<T> entities)
        {
            RepositoryContext.Set<T>().UpdateRange(entities);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            _ = RepositoryContext.Set<T>().Remove(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteBatch(List<T> entities)
        {
            RepositoryContext.Set<T>().RemoveRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Detach(T entity)
        {
            RepositoryContext.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void DetachBatch(List<T> entities)
        {
            foreach(T entity in entities)
            {
                RepositoryContext.Entry(entity).State = EntityState.Detached;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public void DeleteByCondition(Expression<Func<T, bool>> expression)
        {
            RepositoryContext.Set<T>().RemoveRange(RepositoryContext.Set<T>().Where(expression));
        }

        /// <summary>
        /// Method to check if any entity exist for the condition
        /// </summary>
        /// <param name="expression">Expression to search in database</param>
        public bool Any(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).Any();
        }
    }
}