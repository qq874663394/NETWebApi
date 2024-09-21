using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IServices.WebApiDB;
using WebApi.Domain.Services.WebApiDB;
using WebApi.Repositories.WebApiDB;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoleController : BaseController<T_Role>
    {
        protected readonly RoleService _services;
        public RoleController(IRoleServices services) : base(services)
        {
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
