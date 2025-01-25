using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface.IAggregateRoots;
using Domain.Interface.IRepositories;
using Domain.Interface.IRepositories.WebApiDB;

namespace Domain.Interface.IServices
{
    public interface IServices<T>
        where T : class, IEntity,IAggregateRoot
    {
        IRepository<T> Repository { get; }
        // 定义通用服务接口方法
    }
}
