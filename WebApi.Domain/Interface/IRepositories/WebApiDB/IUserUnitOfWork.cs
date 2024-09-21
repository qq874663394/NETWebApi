using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Domain.Interface.IRepositories.WebApiDB
{
    public interface IUserUnitOfWork : IUnitOfWork<T_User>
    {
        IWebApiRepository<T_User> User { get; }
        IWebApiRepository<T_Role> Role { get; }
        IWebApiRepository<T_Org> Org { get; }
        IWebApiRepository<T_UserOrg> UserOrg { get; }
        IWebApiRepository<T_Email> Email { get; }
        IWebApiRepository<T_UserRole> UserRole { get; }
    }
}
