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
    public class RoleUnitOfWork : BaseUnitOfWork<T_Role>, IRoleUnitOfWork
    {
        public IWebApiRepository<T_Role> Role => _role;

        IWebApiRepository<T_Role> _role;
        public RoleUnitOfWork(WebApiRepositoryContext context) : base(context)
        {
            _role = new WebApiRepository<T_Role>(context);
        }

        public void test()
        {
            throw new NotImplementedException();
        }
    }
}
