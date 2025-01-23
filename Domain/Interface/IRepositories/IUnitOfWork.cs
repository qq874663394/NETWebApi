using WebApi.Domain.AggregateRoots.TreeEntity;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Interface.IRepositories
{
    public interface IUnitOfWork<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        int Commit();
        Task<int> CommitAsync();
    }

    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
