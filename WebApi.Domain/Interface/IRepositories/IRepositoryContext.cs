using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Interface.IRepositories
{
    /// <summary>
    /// 表示一个仓储上下文接口，继承自工作单元接口。
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork
    {
        // 用来标识仓储上下文的唯一标识符
        Guid Id { get; }

        /// <summary>
        /// 注册新增实体到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
        /// <param name="entity">待新增的实体</param>
        void RegisterNew<TAggregateRoot>(TAggregateRoot entity)
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 注册修改实体到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
        /// <param name="entity">待修改的实体</param>
        void RegisterModified<TAggregateRoot>(TAggregateRoot entity)
            where TAggregateRoot : class, IAggregateRoot;

        /// <summary>
        /// 注册删除实体到仓储上下文中
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
        /// <param name="entity">待删除的实体</param>
        void RegisterDeleted<TAggregateRoot>(TAggregateRoot entity)
            where TAggregateRoot : class, IAggregateRoot;
    }
}
