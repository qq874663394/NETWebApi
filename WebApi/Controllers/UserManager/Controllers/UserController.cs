using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers._Shared;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IServices;

namespace WebApi.Controllers.UserManager.Controllers
{

    public class UserController : BaseController<T_User>
    {
        public UserController(IServices<T_User> services, ILogger<BaseController<T_User>> logger) : base(services, logger)
        {
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
