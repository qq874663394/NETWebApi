using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Enum;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.ISpecifications;

namespace WebApi.Domain.Interface.IRepositories
{
    // 仓储接口
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        void Add(TEntity entity);

        /// <summary>
        /// 批量新增对象，如果对象Id为空，则会自动创建默认Id
        /// </summary>
        void BatchAdd(TEntity[] entities);
        void Remove(TEntity entity);

        void Delete<T>(Expression<Func<TEntity, bool>> exp);
        void Update(TEntity entity);

        void ExecuteWithTransaction(Action action);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> exp = null);
        IQueryable<TEntity> Query(ISpecification<TEntity> specification);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression);

        #region Synchronization Methods
        DateTime GetDate();
        TResult Query<TResult>(ISpecification<TEntity> where, Expression<Func<TEntity, TResult>> select);
        TResult Query<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> select);
        TResult QueryAsNoTracking<TResult>(ISpecification<TEntity> where, Expression<Func<TEntity, TResult>> select);
        TResult QueryAsNoTracking<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> select);
        TEntity GetByKey<T>(T key);
        TEntity GetBySpecification(ISpecification<TEntity> spec);
        TEntity GetByExpression(Expression<Func<TEntity, bool>> expression);
        TEntity GetBySpecificationAsNoTracking(ISpecification<TEntity> spec);
        TEntity GetByExpressionAsNoTracking(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification);
        IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        IEnumerable<TEntity> GetAllAsNoTracking();
        IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        IEnumerable<TEntity> GetAllAsNoTracking(ISpecification<TEntity> specification);
        IEnumerable<TEntity> GetAllAsNoTracking(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        bool Exists(ISpecification<TEntity> specification);
        bool Exists(Expression<Func<TEntity, bool>> expression);

        int Commit();

        #region 分页支持
        PagedResult<TEntity> GetAll(Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder, int pageIndex, int pageSize);

        PagedResult<TEntity> GetAll(
            ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder, int pageIndex, int pageSize);

        PagedResult<TEntity> GetAll(Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder, int pageIndex, int pageSize,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        PagedResult<TEntity> GetAll(ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder, int pageIndex, int pageSize,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        #endregion

        #endregion

        #region Asynchronous Methods
        Task<DateTime> GetDateAsync();
        Task<TResult> QueryAsync<TResult>(ISpecification<TEntity> where, Expression<Func<TEntity, TResult>> select);
        Task<TResult> QueryAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> select);

        // 根据聚合根的ID值，从仓储中读取聚合根
        Task<TEntity> GetByKeyAsync<T>(T key);

        Task<TEntity> GetBySpecificationAsync(ISpecification<TEntity> spec);

        Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression);

        // 读取所有聚合根。
        Task<IEnumerable<TEntity>> GetAllAsync();

        // 以指定的排序字段和排序方式，从仓储中读取所有聚合根。
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);

        //  根据指定的规约获取聚合根
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);

        // 根据指定的规约,以指定的排序字段和排序方式，从仓储中读取聚合根
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);

        // 返回一个值，该值表示符合指定规约条件的聚合根是否存在。
        Task<bool> ExistsAsync(ISpecification<TEntity> specification);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);

        Task<int> CommitAsync();

        #region 分页支持
        Task<PagedResult<TEntity>> GetAllAsync(Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder, int pageIndex, int pageSize);

        Task<PagedResult<TEntity>> GetAllAsync(
            ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder, int pageIndex, int pageSize);

        Task<PagedResult<TEntity>> GetAllAsync(Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder, int pageIndex, int pageSize,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        Task<PagedResult<TEntity>> GetAllAsync(ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder, int pageIndex, int pageSize,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        #endregion

        #endregion

        #region 饥饿加载方式

        TEntity GetBySpecification(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        Task<TEntity> GetBySpecificationAsync(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        #endregion
    }
}
