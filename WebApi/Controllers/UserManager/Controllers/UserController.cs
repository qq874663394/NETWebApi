using Microsoft.AspNetCore.Mvc;
using Controllers._Shared;
using Domain.Entities;
using Domain.Interface.IServices;
using Microsoft.Extensions.Logging;
using Domain.Interface.IRepositories;
using Domain.Services;
using Domain.Interface.IServices.WebApiDB;
using Domain.Services.WebApiDB;
using Microsoft.AspNetCore.Cors;

namespace Controllers.UserManager.Controllers
{

    [EnableCors("AllowSpecificOrigins")]
    public class UserController : BaseController<T_User>
    {
        UserService _service;
        Lazy<ILogger> _localLogger;
        public UserController(IUserServices services, ILoggerFactory loggerFactory) : base(services, loggerFactory)
        {
            _localLogger = new Lazy<ILogger>(() => loggerFactory.CreateLogger(GetType()));
            _service = services as UserService;
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
