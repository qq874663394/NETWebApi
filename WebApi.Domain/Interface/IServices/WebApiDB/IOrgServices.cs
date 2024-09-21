using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;

namespace WebApi.Domain.Interface.IServices.WebApiDB
{
    public interface IOrgServices : IServices<T_Org>
    {
        // 定义特定于用户服务的方法
    }
}