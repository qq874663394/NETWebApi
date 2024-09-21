using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IRepositories.WebApiDB;
using WebApi.Domain.Interface.IServices.WebApiDB;
using WebApi.Domain.Services.WebApiDB;
using WebApi.Repositories.WebApiDB;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrgController : BaseController<T_Org>
    {
        protected readonly OrgService _services;
        public OrgController(IOrgServices services) : base(services)
        {
            //(_services.UnitOfWork as IOrgUnitOfWork)
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
