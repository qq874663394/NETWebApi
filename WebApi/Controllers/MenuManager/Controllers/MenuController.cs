using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers._Shared;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IServices;

namespace WebApi.Controllers.MenuManager.Controllers
{
    public class MenuController : BaseController<T_Menu>
    {
        public MenuController(IServices<T_Menu> services, ILogger<BaseController<T_Menu>> logger) : base(services, logger)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
