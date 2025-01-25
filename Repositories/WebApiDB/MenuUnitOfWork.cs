using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interface.IRepositories;
using Domain.Interface.IRepositories.WebApiDB;

namespace Repositories.WebApiDB
{
    public class MenuUnitOfWork : BaseUnitOfWork<T_Menu>, IMenuUnitOfWork
    {
        public IWebApiRepository<T_Menu> Menu => _menu;

        public IWebApiRepository<T_Button> Button => _button;

        public IWebApiRepository<T_ButtonPermission> ButtonPermission => _buttonPermission;

        public IWebApiRepository<T_MenuButton> MenuButton => _menuButton;

        IWebApiRepository<T_Menu> _menu;
        IWebApiRepository<T_Button> _button;
        IWebApiRepository<T_ButtonPermission> _buttonPermission;
        IWebApiRepository<T_MenuButton> _menuButton;
        public MenuUnitOfWork(WebApiRepositoryContext context) : base(context)
        {
            _menu = new WebApiRepository<T_Menu>(context);
            _button = new WebApiRepository<T_Button>(context);
            _buttonPermission = new WebApiRepository<T_ButtonPermission>(context);
            _menuButton = new WebApiRepository<T_MenuButton>(context);
        }
    }
}
