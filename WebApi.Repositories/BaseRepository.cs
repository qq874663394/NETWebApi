using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.ISpecifications;
using WebApi.Domain.Specifications;
using WebApi.Domain;
using WebApi.Repositories.WebApiDB;
using WebApi.Domain.Enum;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebApi.Repositories
{
    public abstract class BaseRepository<TEntity> : IWebApiRepository<TEntity>
            where TEntity : class, IEntity
    {
        protected readonly WebApiRepositoryContext _repository;
        protected Lazy<WebApiDbContext> _lazyDbContext => _repository.LazyDbContex;
        //protected UpDbContext _dbContext => _lazyDbContext.Value;

        protected BaseRepository(WebApiRepositoryContext context)
        {
            _repository = context;
        }

        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException("lambda");

            MemberExpression memberExpr = null;

            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpr =
                        ((UnaryExpression)lambda.Body).Operand as MemberExpression;
                    break;
                case ExpressionType.MemberAccess:
                    memberExpr = lambda.Body as MemberExpression;
                    break;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        private string GetEagerLoadingPath(Expression<Func<TEntity, dynamic>> eagerLoadingProperty)
        {
            var memberExpression = this.GetMemberInfo(eagerLoadingProperty);
            var parameterName = eagerLoadingProperty.Parameters.First().Name;
            var memberExpressionStr = memberExpression.ToString();
            var path = memberExpressionStr.Replace(parameterName + ".", "");
            return path;
        }

        public void Add(TEntity entity)
        {
            // 调用IEntityFrameworkRepositoryContext的RegisterNew方法将实体添加进DbContext.DbSet对象中
            _repository.RegisterNew(entity);
        }
        /// <summary>
        /// 批量新增对象，如果对象Id为空，则会自动创建默认Id
        /// </summary>
        public void BatchAdd(TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity.KeyIsNull())
                {
                    entity.GenerateDefaultKeyVal();
                }
            }
            _lazyDbContext.Value.Set<TEntity>().AddRange(entities);
        }
        public void Remove(TEntity entity)
        {
            _repository.RegisterDeleted(entity);
        }
        /// <summary>
        /// 批量删除
        /// <para>该方法内部自动调用了SaveChanges()，需要ExecuteWithTransaction配合才能实现事务控制</para>
        /// </summary>
        public virtual void Delete<T>(Expression<Func<TEntity, bool>> exp)
        {
            var entities = _lazyDbContext.Value.Set<TEntity>().Where(exp);
            _lazyDbContext.Value.Set<TEntity>().RemoveRange(entities);
        }
        /// <summary>
        /// EF默认情况下，每调用一次SaveChanges()都会执行一个单独的事务
        /// 本接口实现在一个事务中可以多次执行SaveChanges()方法
        /// </summary>
        public void ExecuteWithTransaction(Action action)
        {
            using (IDbContextTransaction transaction = _lazyDbContext.Value.Database.BeginTransaction())
            {
                try
                {
                    action();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 根据过滤条件，获取记录
        /// </summary>
        /// <param name="exp">The exp.</param>
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> exp = null)
        {
            return Filter(exp);
        }

        private IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> exp)
        {
            var dbSet = _lazyDbContext.Value.Set<TEntity>().AsNoTracking().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }
        public void Update(TEntity aggregateRoot)
        {
            _repository.RegisterModified(aggregateRoot);
        }

        public virtual IQueryable<TEntity> Query(ISpecification<TEntity> specification)
        {
            return _lazyDbContext.Value.Set<TEntity>().Where(specification.Expression);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            return _lazyDbContext.Value.Set<TEntity>().Where(expression);
        }

        #region 同步方法
        public DateTime GetDate()
        {
            DateTime now = _lazyDbContext.Value.Set<TEntity>().Select(p => DateTime.Now).FirstOrDefault();

            if (now == DateTime.MinValue) now = DateTime.Now;

            return now;
        }

        public virtual TResult Query<TResult>(ISpecification<TEntity> where, Expression<Func<TEntity, TResult>> select)
        {
            return _lazyDbContext.Value.Set<TEntity>().Where(where.Expression).Select(select).FirstOrDefault();
        }

        public virtual TResult QueryAsNoTracking<TResult>(ISpecification<TEntity> where, Expression<Func<TEntity, TResult>> select)
        {
            return _lazyDbContext.Value.Set<TEntity>().AsNoTracking().Where(where.Expression).Select(select).FirstOrDefault();
        }

        public virtual TResult Query<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> select)
        {
            return _lazyDbContext.Value.Set<TEntity>().Where(where).Select(select).FirstOrDefault();
        }

        public virtual TResult QueryAsNoTracking<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> select)
        {
            return _lazyDbContext.Value.Set<TEntity>().AsNoTracking().Where(where).Select(select).FirstOrDefault();
        }

        public virtual TEntity GetByKey<T>(T key)
        {
            return _lazyDbContext.Value.Find<TEntity>(key);
        }

        public TEntity GetBySpecification(ISpecification<TEntity> spec)
        {
            return _lazyDbContext.Value.Set<TEntity>().FirstOrDefault(spec.Expression);
        }

        public TEntity GetBySpecificationAsNoTracking(ISpecification<TEntity> spec)
        {
            return _lazyDbContext.Value.Set<TEntity>().AsNoTracking().FirstOrDefault(spec.Expression);
        }

        public TEntity GetByExpression(Expression<Func<TEntity, bool>> expression)
        {
            return _lazyDbContext.Value.Set<TEntity>().FirstOrDefault(expression);
        }

        public TEntity GetByExpressionAsNoTracking(Expression<Func<TEntity, bool>> expression)
        {
            return _lazyDbContext.Value.Set<TEntity>().AsNoTracking().FirstOrDefault(expression);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetAll(new AnySpecification<TEntity>(), null, SortOrder.UnSpecified);
        }

        public IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification)
        {
            return GetAll(specification, null, SortOrder.UnSpecified);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return GetAll(new AnySpecification<TEntity>(), sortPredicate, sortOrder);
        }

        public IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var query = _lazyDbContext.Value.Set<TEntity>().Where(specification.Expression);
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.OrderBy(sortPredicate).ToList();
                    case SortOrder.Descending:
                        return query.OrderByDescending(sortPredicate).ToList();
                    default:
                        break;
                }
            }

            return query.ToList();
        }

        public IEnumerable<TEntity> GetAllAsNoTracking()
        {
            return GetAllAsNoTracking(new AnySpecification<TEntity>(), null, SortOrder.UnSpecified);
        }

        public IEnumerable<TEntity> GetAllAsNoTracking(ISpecification<TEntity> specification)
        {
            return GetAllAsNoTracking(specification, null, SortOrder.UnSpecified);
        }

        public IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return GetAllAsNoTracking(new AnySpecification<TEntity>(), sortPredicate, sortOrder);
        }

        public IEnumerable<TEntity> GetAllAsNoTracking(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var query = _lazyDbContext.Value.Set<TEntity>().AsNoTracking().Where(specification.Expression);
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.OrderBy(sortPredicate).ToList();
                    case SortOrder.Descending:
                        return query.OrderByDescending(sortPredicate).ToList();
                    default:
                        break;
                }
            }

            return query.ToList();
        }

        public bool Exists(ISpecification<TEntity> specification)
        {
            var count = _lazyDbContext.Value.Set<TEntity>().Count(specification.IsSatisfiedBy);
            return count != 0;
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _lazyDbContext.Value.Set<TEntity>().Any(expression);
        }

        #region 提交数据
        public int Commit()
        {
            return _lazyDbContext.Value.SaveChanges();
        }
        #endregion

        #region 饥饿加载方式实现
        public TEntity GetBySpecification(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = _lazyDbContext.Value.Set<TEntity>();
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                return dbquery.Where(specification.Expression).FirstOrDefault();
            }
            else
                return dbset.Where(specification.Expression).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return GetAll(new AnySpecification<TEntity>(), null, SortOrder.UnSpecified, eagerLoadingProperties);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return GetAll(new AnySpecification<TEntity>(), sortPredicate, sortOrder, eagerLoadingProperties);
        }

        public IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return GetAll(specification, null, SortOrder.UnSpecified, eagerLoadingProperties);
        }

        public IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = _lazyDbContext.Value.Set<TEntity>();
            IQueryable<TEntity> queryable = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.Expression);
            }
            else
                queryable = dbset.Where(specification.Expression);

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return queryable.OrderBy(sortPredicate).ToList();
                    case SortOrder.Descending:
                        return queryable.OrderByDescending(sortPredicate).ToList();
                    default:
                        break;
                }
            }
            return queryable.ToList();
        }
        #endregion

        #region 分页支持
        public PagedResult<TEntity> GetAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageIndex, int pageSize)
        {
            return GetAll(new AnySpecification<TEntity>(), sortPredicate, sortOrder, pageIndex, pageSize);
        }

        // 分页也就是每次只取出每页展示的数据大小
        public PagedResult<TEntity> GetAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0)
                throw new ArgumentOutOfRangeException("pageIndex", pageIndex, "页码必须大于等于1");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1");

            var query = _lazyDbContext.Value.Set<TEntity>()
                .Where(specification.Expression);
            var skip = (pageIndex - 1) * pageSize;
            var take = pageSize;
            if (sortPredicate == null)
                throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");

            int count = query.Count();

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    //var pagedGroupAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = count }).FirstOrDefault();
                    var pagedGroupAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take);
                    if (pagedGroupAscending == null)
                        return null;
                    //return new PagedResult<TEntity>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupAscending.Select(p => p).ToList());
                    return new PagedResult<TEntity>(count, (count + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupAscending.ToList());
                case SortOrder.Descending:
                    //var pagedGroupDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = count }).FirstOrDefault();
                    var pagedGroupDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take);
                    if (pagedGroupDescending == null)
                        return null;
                    //return new PagedResult<TEntity>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupDescending.Select(p => p).ToList());
                    return new PagedResult<TEntity>(count, (count + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupDescending.ToList());
                default:
                    break;
            }

            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }

        public PagedResult<TEntity> GetAll(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageIndex, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return GetAll(new AnySpecification<TEntity>(), sortPredicate, sortOrder, pageIndex, pageSize, eagerLoadingProperties);
        }

        public PagedResult<TEntity> GetAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageIndex, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            if (pageIndex <= 0)
                throw new ArgumentOutOfRangeException("pageIndex", pageIndex, "页码必须大于等于1");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1");

            // 将需要饥饿加载的内容添加到Include方法参数中
            var dbset = _lazyDbContext.Value.Set<TEntity>();

            IQueryable<TEntity> query = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }

                query = dbquery.Where(specification.Expression);
            }
            else
                query = dbset.Where(specification.Expression);

            var skip = (pageIndex - 1) * pageSize;
            var take = pageSize;

            if (sortPredicate == null)
                throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");

            int count = query.Count();

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    //var pagedGroupAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = count }).FirstOrDefault();
                    var pagedGroupAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take);
                    if (pagedGroupAscending == null)
                        return null;
                    //return new PagedResult<TEntity>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupAscending.Select(p => p).ToList());
                    return new PagedResult<TEntity>(count, (count + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupAscending.ToList());
                case SortOrder.Descending:
                    //var pagedGroupDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = count }).FirstOrDefault();
                    var pagedGroupDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take);
                    if (pagedGroupDescending == null)
                        return null;
                    //return new PagedResult<TEntity>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupDescending.Select(p => p).ToList());
                    return new PagedResult<TEntity>(count, (count + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupDescending.ToList());
                default:
                    break;
            }

            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }

        #endregion

        #endregion

        #region 异步方法
        public async Task<DateTime> GetDateAsync()
        {
            return await _lazyDbContext.Value.Set<TEntity>().Select(p => DateTime.Now).FirstOrDefaultAsync();
        }

        public async virtual Task<TResult> QueryAsync<TResult>(ISpecification<TEntity> where, Expression<Func<TEntity, TResult>> select)
        {
            return await _lazyDbContext.Value.Set<TEntity>().Where(where.Expression).Select(select).FirstOrDefaultAsync();
        }

        public async virtual Task<TResult> QueryAsync<TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TResult>> select)
        {
            return await _lazyDbContext.Value.Set<TEntity>().Where(where).Select(select).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetByKeyAsync<T>(T key)
        {
            return await _lazyDbContext.Value.FindAsync<TEntity>(key);
        }

        public async Task<TEntity> GetBySpecificationAsync(ISpecification<TEntity> spec)
        {
            return await _lazyDbContext.Value.Set<TEntity>().FirstOrDefaultAsync(spec.Expression);
        }

        public async Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _lazyDbContext.Value.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetAllAsync(new AnySpecification<TEntity>(), null, SortOrder.UnSpecified);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification)
        {
            return await GetAllAsync(specification, null, SortOrder.UnSpecified);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return await GetAllAsync(new AnySpecification<TEntity>(), sortPredicate, sortOrder);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var query = _lazyDbContext.Value.Set<TEntity>().Where(specification.Expression);
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.OrderBy(sortPredicate).ToList();
                    case SortOrder.Descending:
                        return query.OrderByDescending(sortPredicate).ToList();
                    default:
                        break;
                }
            }

            return await query.ToListAsync();
        }

        public async Task<bool> ExistsAsync(ISpecification<TEntity> specification)
        {
            var count = await _lazyDbContext.Value.Set<TEntity>().CountAsync(specification.Expression);
            return count != 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _lazyDbContext.Value.Set<TEntity>().AnyAsync(expression);
        }

        #region 提交数据
        public async Task<int> CommitAsync()
        {
            return await _lazyDbContext.Value.SaveChangesAsync();
        }
        #endregion

        #region 饥饿加载方式实现
        public async Task<TEntity> GetBySpecificationAsync(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = _lazyDbContext.Value.Set<TEntity>();
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                return await dbquery.Where(specification.Expression).FirstOrDefaultAsync();
            }
            else
                return await dbset.Where(specification.Expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return await GetAllAsync(new AnySpecification<TEntity>(), null, SortOrder.UnSpecified, eagerLoadingProperties);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return await GetAllAsync(new AnySpecification<TEntity>(), sortPredicate, sortOrder, eagerLoadingProperties);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return await GetAllAsync(specification, null, SortOrder.UnSpecified, eagerLoadingProperties);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            var dbset = _lazyDbContext.Value.Set<TEntity>();
            IQueryable<TEntity> queryable = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.Expression);
            }
            else
                queryable = dbset.Where(specification.Expression);

            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return await queryable.OrderBy(sortPredicate).ToListAsync();
                    case SortOrder.Descending:
                        return await queryable.OrderByDescending(sortPredicate).ToListAsync();
                    default:
                        break;
                }
            }
            return await queryable.ToListAsync();
        }
        #endregion

        #region 分页支持
        public async Task<PagedResult<TEntity>> GetAllAsync(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageIndex, int pageSize)
        {
            return await GetAllAsync(new AnySpecification<TEntity>(), sortPredicate, sortOrder, pageIndex, pageSize);
        }

        // 分页也就是每次只取出每页展示的数据大小
        public async Task<PagedResult<TEntity>> GetAllAsync(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0)
                throw new ArgumentOutOfRangeException("pageIndex", pageIndex, "页码必须大于等于1");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1");

            var query = _lazyDbContext.Value.Set<TEntity>()
                .Where(specification.Expression);
            var skip = (pageIndex - 1) * pageSize;
            var take = pageSize;
            if (sortPredicate == null)
                throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");

            int count = await query.CountAsync();

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    //var pagedGroupAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = count }).FirstOrDefault();
                    var pagedGroupAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take);
                    if (pagedGroupAscending == null)
                        return null;
                    //return new PagedResult<TEntity>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupAscending.Select(p => p).ToList());
                    return new PagedResult<TEntity>(count, (count + pageSize - 1) / pageSize, pageSize, pageIndex, await pagedGroupAscending.ToListAsync());
                case SortOrder.Descending:
                    //var pagedGroupDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = count }).FirstOrDefault();
                    var pagedGroupDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take);
                    if (pagedGroupDescending == null)
                        return null;
                    //return new PagedResult<TEntity>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupDescending.Select(p => p).ToList());
                    return new PagedResult<TEntity>(count, (count + pageSize - 1) / pageSize, pageSize, pageIndex, await pagedGroupDescending.ToListAsync());
                default:
                    break;
            }

            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }

        public async Task<PagedResult<TEntity>> GetAllAsync(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageIndex, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return await GetAllAsync(new AnySpecification<TEntity>(), sortPredicate, sortOrder, pageIndex, pageSize, eagerLoadingProperties);
        }

        public async Task<PagedResult<TEntity>> GetAllAsync(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageIndex, int pageSize, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            if (pageIndex <= 0)
                throw new ArgumentOutOfRangeException("pageIndex", pageIndex, "页码必须大于等于1");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1");

            // 将需要饥饿加载的内容添加到Include方法参数中
            var dbset = _lazyDbContext.Value.Set<TEntity>();

            IQueryable<TEntity> query = null;
            if (eagerLoadingProperties != null &&
                eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbset.Include(eagerLoadingPath);
                for (var i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }

                query = dbquery.Where(specification.Expression);
            }
            else
                query = dbset.Where(specification.Expression);

            var skip = (pageIndex - 1) * pageSize;
            var take = pageSize;

            if (sortPredicate == null)
                throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");

            int count = await query.CountAsync();

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    //var pagedGroupAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = count }).FirstOrDefault();
                    var pagedGroupAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take);
                    if (pagedGroupAscending == null)
                        return null;
                    //return new PagedResult<TEntity>(pagedGroupAscending.Key.Total, (pagedGroupAscending.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupAscending.Select(p => p).ToList());
                    return new PagedResult<TEntity>(count, (count + pageSize - 1) / pageSize, pageSize, pageIndex, await pagedGroupAscending.ToListAsync());
                case SortOrder.Descending:
                    //var pagedGroupDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take).GroupBy(p => new { Total = count }).FirstOrDefault();
                    var pagedGroupDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take);
                    if (pagedGroupDescending == null)
                        return null;
                    //return new PagedResult<TEntity>(pagedGroupDescending.Key.Total, (pagedGroupDescending.Key.Total + pageSize - 1) / pageSize, pageSize, pageIndex, pagedGroupDescending.Select(p => p).ToList());
                    return new PagedResult<TEntity>(count, (count + pageSize - 1) / pageSize, pageSize, pageIndex, await pagedGroupDescending.ToListAsync());
                default:
                    break;
            }

            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }
        #endregion

        #endregion
    }
}
