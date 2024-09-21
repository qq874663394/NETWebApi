using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IServices;
using WebApi.Domain.Interface.IServices.WebApiDB;
using WebApi.Domain.Model;
using WebApi.Domain.Services;
using WebApi.Domain.Services.WebApiDB;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : BaseController<T_User>
    {
        protected readonly IUserServices _services;
        protected readonly IAuthServices _auth;
        public UserController(IUserServices services, IAuthServices auth) : base(services)
        {
            _auth = auth;
            _services = services;
        }
        [HttpGet]
        [ServiceFilter(typeof(JwtTokenFilterAttribute))]
        public IActionResult GetInfo()
        {
            return null;
            // 从 HttpContext 中获取 JWT Token 的值
            //var jwtToken = HttpContext.Items["JwtToken"] as JwtToken;
            //return jwtToken;
            //if (jwtToken != null)
            //{
            //    var entity = _services(jwtToken.UserId);
            //    return Ok(entity);
            //}
            //else
            //{
            //    return BadRequest();
            //}
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Auth(string userId, string pwd)
        {
            var entity = _services.Repository.GetAllAsync();
            if (entity == null)
            {
                return BadRequest();
            }
            if (!pwd.Equals(entity.Result.FirstOrDefault().Password))
            {
                return BadRequest();
            }
            return new JsonResult(_auth.CreateToken(userId));
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
