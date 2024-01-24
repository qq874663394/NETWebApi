using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IUnitOfWorks;

namespace WebApi.Repositories.WebApiDB
{
    public class BaseUnitOfWork<TAggregateRoot> : IUnitOfWork<TAggregateRoot>
           where TAggregateRoot : IAggregateRoot
    {
        protected readonly WebApiRepositoryContext _repository;
        protected Lazy<WebApiDbContext> _lazyDbContext => _repository.LazyDbContex;
        protected WebApiDbContext _dbContext => _lazyDbContext.Value;

        public BaseUnitOfWork(WebApiRepositoryContext context)
        {
            _repository = context;
        }

        #region 提交数据
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        #endregion
    }

    public class BaseUnitOfWork : IUnitOfWork
    {
        protected readonly WebApiRepositoryContext _repository;
        protected Lazy<WebApiDbContext> _lazyDbContext => _repository.LazyDbContex;
        protected WebApiDbContext _dbContext => _lazyDbContext.Value;

        public BaseUnitOfWork(WebApiRepositoryContext context)
        {
            _repository = context;
        }

        #region 提交数据
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
