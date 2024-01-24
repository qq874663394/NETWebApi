using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities.WebApiDB;
using WebApi.Domain.Interface.IUnitOfWorks.WebApiDB;

namespace WebApi.Domain.Interface.IServices.WebApiDB
{
    public interface IRelevanceService
    {
        IRelevanceUnitOfWork UnitOfWork { get; }
    }
}
