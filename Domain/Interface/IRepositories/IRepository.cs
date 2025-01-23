using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.AggregateRoots.TreeEntity;
using WebApi.Domain.Enum;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.ISpecifications;

namespace WebApi.Domain.Interface.IRepositories
{
    /// <summary>
    /// 仓储接口，定义了对实体进行 CRUD 操作的方法。
    /// </summary>
    /// <typeparam name="TEntity">实体类型，必须是 IEntity 接口的实现类。</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        #region 同步方法
        // 向仓储中添加实体
        void Add(TEntity entity);

        /// <summary>
        /// 批量新增对象，如果对象Id为空，则会自动创建默认Id
        /// </summary>
        void BatchAdd(TEntity[] entities);

        // 从仓储中删除实体
        void Remove(TEntity entity);

        // 根据条件从仓储中删除实体
        void Delete<T>(Expression<Func<TEntity, bool>> exp);

        // 更新仓储中的实体
        void Update(TEntity entity);

        // 在事务中执行操作
        void ExecuteWithTransaction(Action action);

        // 根据条件查询实体集合
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> exp = null);

        // 使用规范查询实体集合
        IQueryable<TEntity> Query(ISpecification<TEntity> specification);

        // 使用表达式查询实体集合
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression);

        // 获取当前时间
        DateTime GetDate();

        // 使用规范和选择器查询结果
        TResult Query<TResult>(ISpecification<TEntity> where, Expression<Func<TEntity, TResult>> select);

        // 使用表达式和选择器查询结果
        TResult Query<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> select);

        // 使用 ID 获取实体
        TEntity GetByKey<T>(T key);

        // 使用规范获取实体
        TEntity GetBySpecification(ISpecification<TEntity> spec);

        // 使用表达式获取实体
        TEntity GetByExpression(Expression<Func<TEntity, bool>> expression);

        // 使用规范获取实体（不跟踪）
        TEntity GetBySpecificationAsNoTracking(ISpecification<TEntity> spec);

        // 使用表达式获取实体（不跟踪）
        TEntity GetByExpressionAsNoTracking(Expression<Func<TEntity, bool>> expression);

        // 获取所有实体
        IEnumerable<TEntity> GetAll();

        // 获取所有实体，并按指定排序方式排序
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);

        // 使用规范获取所有实体
        IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification);

        // 使用规范获取所有实体，并按指定排序方式排序
        IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);

        // 获取所有实体（不跟踪）
        IEnumerable<TEntity> GetAllAsNoTracking();

        // 获取所有实体（不跟踪），并按指定排序方式排序
        IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);

        // 使用规范获取所有实体（不跟踪）
        IEnumerable<TEntity> GetAllAsNoTracking(ISpecification<TEntity> specification);

        // 使用规范获取所有实体（不跟踪），并按指定排序方式排序
        IEnumerable<TEntity> GetAllAsNoTracking(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);

        // 判断是否存在满足条件的实体（使用规范）
        bool Exists(ISpecification<TEntity> specification);

        // 判断是否存在满足条件的实体（使用表达式）
        bool Exists(Expression<Func<TEntity, bool>> expression);

        // 提交工作单元
        int Commit();

        #endregion

        #region 获取分页结果
        // 分页获取所有聚合根，根据指定的排序字段和排序方式
        PagedResult<TEntity> GetAll(
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder,
            int pageIndex,
            int pageSize);

        // 根据规约获取分页结果，同时指定排序字段和排序方式
        PagedResult<TEntity> GetAll(
            ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder,
            int pageIndex,
            int pageSize);

        // 分页获取所有聚合根，根据指定的排序字段和排序方式，同时进行预加载操作
        PagedResult<TEntity> GetAll(
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder,
            int pageIndex,
            int pageSize,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        // 根据规约获取分页结果，同时指定排序字段和排序方式，并进行预加载操作
        PagedResult<TEntity> GetAll(
            ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder,
            int pageIndex,
            int pageSize,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        #endregion

        #region 异步方法
        // 异步获取当前时间
        Task<DateTime> GetDateAsync();

        // 使用规范和选择器异步查询结果
        Task<TResult> QueryAsync<TResult>(ISpecification<TEntity> where, Expression<Func<TEntity, TResult>> select);

        // 使用表达式和选择器异步查询结果
        Task<TResult> QueryAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> select);

        // 根据聚合根的ID值异步从仓储中读取聚合根
        Task<TEntity> GetByKeyAsync<T>(T key);

        // 根据规范异步获取实体
        Task<TEntity> GetBySpecificationAsync(ISpecification<TEntity> spec);

        // 根据表达式异步获取实体
        Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression);

        // 异步读取所有聚合根
        Task<IEnumerable<TEntity>> GetAllAsync();

        // 异步以指定的排序字段和排序方式从仓储中读取所有聚合根
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);

        // 异步根据指定的规约获取聚合根
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);

        // 异步根据指定的规约、排序字段和排序方式从仓储中读取聚合根
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, 
            Expression<Func<TEntity, dynamic>> sortPredicate, 
            SortOrder sortOrder);

        // 异步返回一个值，该值表示符合指定规约条件的聚合根是否存在
        Task<bool> ExistsAsync(ISpecification<TEntity> specification);

        // 异步返回一个值，该值表示符合指定表达式条件的聚合根是否存在
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);

        // 异步提交工作单元
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

        // 使用规约获取实体，并进行预加载操作
        TEntity GetBySpecification(
            ISpecification<TEntity> specification,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        // 获取所有实体，并进行预加载操作
        IEnumerable<TEntity> GetAll(
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        // 获取所有实体，并按指定排序方式排序，并进行预加载操作
        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        // 使用规约获取所有实体，并进行预加载操作
        IEnumerable<TEntity> GetAll(
            ISpecification<TEntity> specification,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        // 使用规约获取所有实体，并按指定排序方式排序，并进行预加载操作
        IEnumerable<TEntity> GetAll(
            ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        // 异步使用规约获取实体，并进行预加载操作
        Task<TEntity> GetBySpecificationAsync(
            ISpecification<TEntity> specification,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        // 异步获取所有实体，并进行预加载操作
        Task<IEnumerable<TEntity>> GetAllAsync(
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        // 异步获取所有实体，并按指定排序方式排序，并进行预加载操作
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        // 异步使用规约获取所有实体，并进行预加载操作
        Task<IEnumerable<TEntity>> GetAllAsync(
            ISpecification<TEntity> specification,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        // 异步使用规约获取所有实体，并按指定排序方式排序，并进行预加载操作
        Task<IEnumerable<TEntity>> GetAllAsync(
            ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> sortPredicate,
            SortOrder sortOrder,
            params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);
        #endregion
    }
}