using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IRepositories.WebApiDB;

namespace WebApi.Domain.Interface.IServices
{
    public interface IServices<T>
        where T : class, IEntity,IAggregateRoot
    {
        IRepository<T> Repository { get; }
        // 定义通用服务接口方法
    }
}
