using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface.IAggregateRoots;
using Domain.Interface.IRepositories;

namespace Repositories.WebApiDB
{
    public class WebApiRepository<TEntity> : BaseRepository<TEntity>
        where TEntity : class, IEntity
    {
        public WebApiRepository(WebApiRepositoryContext context) : base(context)
        {

        }

    }
}
