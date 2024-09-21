using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Interface.IRepositories.WebApiDB
{
    public interface IOrgUnitOfWork : IUnitOfWork<T_Org>
    {
        IWebApiRepository<T_Org> Org { get; }
    }
}
