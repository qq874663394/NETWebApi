using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interface.IRepositories.WebApiDB
{
    public interface IMenuUnitOfWork : IUnitOfWork<T_Menu>
    {
        IWebApiRepository<T_Menu> Menu { get; }
        IWebApiRepository<T_Button> Button { get; }
        IWebApiRepository<T_ButtonPermission> ButtonPermission { get; }
        IWebApiRepository<T_MenuButton> MenuButton { get; }
    }
}
