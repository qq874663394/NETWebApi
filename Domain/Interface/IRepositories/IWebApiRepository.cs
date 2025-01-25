using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface.IAggregateRoots;

namespace Domain.Interface.IRepositories
{
    public interface IWebApiRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
    }
}
