using WebApi.Domain.AggregateRoots.TreeEntity;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Interface.IRepositories
{
    public interface IUnitOfWork<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        int Commit();
        Task<int> CommitAsync();

        /// <summary>
        /// 更新树状结构实体
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="U"></typeparam>
        public void UpdateTreeObj<U>(U obj) where U : TreeEntity;

        /// <summary>
        /// 计算实体更新的层级信息
        /// </summary>
        /// <typeparam name="U">U必须是一个继承TreeEntity的结构</typeparam>
        /// <param name="entity"></param>
        public void CaculateCascade<U>(U entity) where U : TreeEntity;
    }

    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();

        /// <summary>
        /// 更新树状结构实体
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="U"></typeparam>
        public void UpdateTreeObj<U>(U obj) where U : TreeEntity;

        /// <summary>
        /// 计算实体更新的层级信息
        /// </summary>
        /// <typeparam name="U">U必须是一个继承TreeEntity的结构</typeparam>
        /// <param name="entity"></param>
        public void CaculateCascade<U>(U entity) where U : TreeEntity;
    }
}
