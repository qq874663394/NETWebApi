using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IRepositories.WebApiDB;

namespace WebApi.Domain.Interface.IServices.WebApiDB
{
    public interface IUserServices : IServices<T_User>
    {
        public IUserUnitOfWork UnitOfWork { get; }
        // 定义特定于用户服务的方法
    }
}