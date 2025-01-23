using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Interface.IRepositories
{
    public interface IWebApiRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
    }
}
