using Domain.AggregateRoots.TreeEntity;
using Domain.Interface.IAggregateRoots;

namespace Domain.Interface.IRepositories
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
