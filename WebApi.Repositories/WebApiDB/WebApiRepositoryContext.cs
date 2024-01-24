using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Repositories.WebApiDB
{
    public class WebApiRepositoryContext
    {
        // ThreadLocal代表线程本地存储，主要相当于一个静态变量
        // 但静态变量在多线程访问时需要显式使用线程同步技术。
        // 使用ThreadLocal变量，每个线程都会一个拷贝，从而避免了线程同步带来的性能开销

        private readonly ThreadLocal<Lazy<WebApiDbContext>> _localCtx;
        private readonly Guid _id;

        public WebApiRepositoryContext(Lazy<WebApiDbContext> context)
        {
            _localCtx = new ThreadLocal<Lazy<WebApiDbContext>>(() => context);
            _id = Guid.NewGuid();
        }

        public Lazy<WebApiDbContext> LazyDbContex => _localCtx.Value;

        #region IRepositoryContext Members
        public Guid Id
        {
            get { return _id; }
        }

        public void RegisterNew<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _localCtx.Value.Value.Set<TEntity>().Add(entity);
        }

        public void RegisterModified<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _localCtx.Value.Value.Entry(entity).State = EntityState.Modified;
        }

        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _localCtx.Value.Value.Set<TEntity>().Remove(entity);
        }

        #endregion 

        #region IUnitOfWork Members
        public void Commit()
        {
            _localCtx.Value.Value.SaveChanges();
        }
        #endregion 
    }
}
