using Microsoft.AspNetCore.Mvc;
using Controllers._Shared;
using Domain.Entities;
using Domain.Interface.IAggregateRoots;
using Domain.Interface.IServices;
using Microsoft.Extensions.Logging;
using Domain.Interface.IRepositories;
using Domain.Services;
using Domain.Services.WebApiDB;
using Domain.Interface.IServices.WebApiDB;
using Microsoft.AspNetCore.Cors;

namespace Controllers.MenuManager.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class MenuController : BaseController<T_Menu>
    {
        MenuService _service;
        Lazy<ILogger> _localLogger;
        public MenuController(IMenuServices services, ILoggerFactory loggerFactory) : base(services, loggerFactory)
        {
            _localLogger = new Lazy<ILogger>(() => loggerFactory.CreateLogger(GetType()));
            _service = services as MenuService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
