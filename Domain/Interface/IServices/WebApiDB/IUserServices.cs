using Domain.Entities;
using Domain.Interface.IAggregateRoots;
using Domain.Interface.IRepositories;
using Domain.Interface.IRepositories.WebApiDB;

namespace Domain.Interface.IServices.WebApiDB
{
    public interface IUserServices : IServices<T_User>
    {
        public IUserUnitOfWork UnitOfWork { get; }
        // 定义特定于用户服务的方法
    }
}