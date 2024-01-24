using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Interface.IUnitOfWorks
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
