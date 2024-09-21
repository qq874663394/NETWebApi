using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.AggregateRoots.TreeEntity;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;

namespace WebApi.Repositories.WebApiDB
{
    public class BaseUnitOfWork<TAggregateRoot> : IUnitOfWork<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        public readonly WebApiRepositoryContext _repository;
        public Lazy<WebApiDbContext> _lazyDbContext => _repository.LazyDbContex;
        public WebApiDbContext _dbContext => _lazyDbContext.Value;

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

        /// <summary>
        /// 更新树状结构实体
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="U"></typeparam>
        public void UpdateTreeObj<U>(U obj) where U : TreeEntity
        {
            CaculateCascade(obj);

            //获取旧的的CascadeId
            var cascadeId = _dbContext.Set<U>().FirstOrDefault(o => o.Id == obj.Id).CascadeId;
            //根据CascadeId查询子部门
            var objs = _dbContext.Set<U>().Where(u => u.CascadeId.Contains(cascadeId) && u.Id != obj.Id)
                .OrderBy(u => u.CascadeId).ToList();

            //更新操作
            _dbContext.Set<U>().Update(obj);

            //更新子模块的CascadeId
            foreach (var a in objs)
            {
                a.CascadeId = a.CascadeId.Replace(cascadeId, obj.CascadeId);
                if (a.ParentId == obj.Id.ToString())
                {
                    a.ParentName = obj.Name;
                }

                _dbContext.Set<U>().Update(a);
            }
            //_dbContext.SaveChanges();
        }

        /// <summary>
        /// 计算实体更新的层级信息
        /// </summary>
        /// <typeparam name="U">U必须是一个继承TreeEntity的结构</typeparam>
        /// <param name="entity"></param>
        public void CaculateCascade<U>(U entity) where U : TreeEntity
        {
            if (entity.ParentId == "") entity.ParentId = null;
            string cascadeId;
            int currentCascadeId = 1; //当前结点的级联节点最后一位
            var sameLevels = _dbContext.Set<U>().Where(o => o.ParentId == entity.ParentId && o.Id != entity.Id);
            foreach (var obj in sameLevels)
            {
                int objCascadeId = int.Parse(obj.CascadeId.TrimEnd('.').Split('.').Last());
                if (currentCascadeId <= objCascadeId) currentCascadeId = objCascadeId + 1;
            }

            if (!string.IsNullOrEmpty(entity.ParentId))
            {
                var parentOrg = _dbContext.Set<U>().FirstOrDefault<U>(o => o.Id.ToString() == entity.ParentId);
                if (parentOrg != null)
                {
                    cascadeId = parentOrg.CascadeId + currentCascadeId + ".";
                    entity.ParentName = parentOrg.Name;
                }
                else
                {
                    throw new Exception("未能找到该组织的父节点信息");
                }
            }
            else
            {
                cascadeId = ".0." + currentCascadeId + ".";
                entity.ParentName = "根节点";
            }

            entity.CascadeId = cascadeId;
        }
    }

    public class BaseUnitOfWork : IUnitOfWork
    {
        public readonly WebApiRepositoryContext _repository;
        public Lazy<WebApiDbContext> _lazyDbContext => _repository.LazyDbContex;
        public WebApiDbContext _dbContext => _lazyDbContext.Value;

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

        /// <summary>
        /// 更新树状结构实体
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="U"></typeparam>
        public void UpdateTreeObj<U>(U obj) where U : TreeEntity
        {
            CaculateCascade(obj);

            //获取旧的的CascadeId
            var cascadeId = _dbContext.Set<U>().FirstOrDefault(o => o.Id.ToString() == obj.Id.ToString()).CascadeId;
            //根据CascadeId查询子部门
            var objs = _dbContext.Set<U>().Where(u => u.CascadeId.Contains(cascadeId) && u.Id != obj.Id)
                .OrderBy(u => u.CascadeId).ToList();

            //更新操作
            _dbContext.Set<U>().Update(obj);

            //更新子模块的CascadeId
            foreach (var a in objs)
            {
                a.CascadeId = a.CascadeId.Replace(cascadeId, obj.CascadeId);
                if (a.ParentId == obj.Id.ToString())
                {
                    a.ParentName = obj.Name;
                }

                _dbContext.Set<U>().Update(a);
            }
            //_dbContext.SaveChanges();
        }

        /// <summary>
        /// 计算实体更新的层级信息
        /// </summary>
        /// <typeparam name="U">U必须是一个继承TreeEntity的结构</typeparam>
        /// <param name="entity"></param>
        public void CaculateCascade<U>(U entity) where U : TreeEntity
        {
            if (entity.ParentId == "") entity.ParentId = null;
            string cascadeId;
            int currentCascadeId = 1; //当前结点的级联节点最后一位
            var sameLevels = _dbContext.Set<U>().Where(o => o.ParentId == entity.ParentId && o.Id != entity.Id);
            foreach (var obj in sameLevels)
            {
                int objCascadeId = int.Parse(obj.CascadeId.TrimEnd('.').Split('.').Last());
                if (currentCascadeId <= objCascadeId) currentCascadeId = objCascadeId + 1;
            }

            if (!string.IsNullOrEmpty(entity.ParentId))
            {
                var parentOrg = _dbContext.Set<U>().FirstOrDefault<U>(o => o.Id.ToString() == entity.ParentId);
                if (parentOrg != null)
                {
                    cascadeId = parentOrg.CascadeId + currentCascadeId + ".";
                    entity.ParentName = parentOrg.Name;
                }
                else
                {
                    throw new Exception("未能找到该组织的父节点信息");
                }
            }
            else
            {
                cascadeId = ".0." + currentCascadeId + ".";
                entity.ParentName = "根节点";
            }

            entity.CascadeId = cascadeId;
        }
    }
}
