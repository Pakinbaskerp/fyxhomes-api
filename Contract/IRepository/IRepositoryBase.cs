using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API
{
    /// <summary>
    /// Interface <c>IRepositoryBase</c> used to define the methods related for repository base.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<T> FindAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        int FindCountByCondition(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T FindFirstByCondition(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void CreateBatch(List<T> entities);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void UpdateBatch(List<T> entities);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void DeleteBatch(List<T> entities);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Detach(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void DetachBatch(List<T> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        void DeleteByCondition(Expression<Func<T, bool>> expression);
        
        /// <summary>
        /// Method to check if any entity exist for the condition
        /// </summary>
        /// <param name="expression">Expression to search in database</param>
        bool Any(Expression<Func<T, bool>> expression);
    }
}