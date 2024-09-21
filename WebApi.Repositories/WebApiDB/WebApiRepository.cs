using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;

namespace WebApi.Repositories.WebApiDB
{
    public class WebApiRepository<TEntity> : BaseRepository<TEntity>
        where TEntity : class, IEntity
    {
        public WebApiRepository(WebApiRepositoryContext context) : base(context)
        {

        }

    }
}
