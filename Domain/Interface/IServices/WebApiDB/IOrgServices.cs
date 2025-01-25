using Domain.Entities;
using Domain.Interface.IAggregateRoots;
using Domain.Interface.IRepositories;

namespace Domain.Interface.IServices.WebApiDB
{
    public interface IOrgServices : IServices<T_Org>
    {
        // 定义特定于用户服务的方法
    }
}