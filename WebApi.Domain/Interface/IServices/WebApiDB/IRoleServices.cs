using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;

namespace WebApi.Domain.Interface.IServices.WebApiDB
{
    public interface IRoleServices : IServices<T_Role>
    {
        // 定义特定于用户服务的方法
    }
}