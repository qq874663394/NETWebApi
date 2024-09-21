using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IRepositories.WebApiDB;

namespace WebApi.Repositories.WebApiDB
{
    public class OrgUnitOfWork : BaseUnitOfWork<T_Org>, IOrgUnitOfWork
    {
        public IWebApiRepository<T_Org> Org => _org;

        IWebApiRepository<T_Org> _org;
        public OrgUnitOfWork(WebApiRepositoryContext context) : base(context)
        {
            _org = new WebApiRepository<T_Org>(context);
        }

        public void test()
        {
            throw new NotImplementedException();
        }
    }
}
