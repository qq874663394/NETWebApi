using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities.WebApiDB;
using WebApi.Domain.Interface.IRepositories;

namespace WebApi.Domain.Interface.IUnitOfWorks.WebApiDB
{
    public interface IRelevanceUnitOfWork : IUnitOfWork<T_Relevance>
    {
        IWebApiRepository<T_Module> Module { get; }
        IWebApiRepository<T_Relevance> Relevance { get; }
    }
}
