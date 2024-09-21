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
    public class UserUnitOfWork : BaseUnitOfWork<T_User>, IUserUnitOfWork
    {
        public IWebApiRepository<T_User> User => _user;
        IWebApiRepository<T_User> _user;
        public IWebApiRepository<T_Role> Role => _role;
        IWebApiRepository<T_Role> _role;
        public IWebApiRepository<T_Org> Org => _org;
        IWebApiRepository<T_Org> _org;
        public IWebApiRepository<T_UserOrg> UserOrg => _userOrg;
        IWebApiRepository<T_UserOrg> _userOrg;
        public IWebApiRepository<T_Email> Email => _email;
        IWebApiRepository<T_Email> _email;
        public IWebApiRepository<T_UserRole> UserRole => _userRole;
        IWebApiRepository<T_UserRole> _userRole;
        public UserUnitOfWork(WebApiRepositoryContext context) : base(context)
        {
            _user = new WebApiRepository<T_User>(context);
        }
    }
}
