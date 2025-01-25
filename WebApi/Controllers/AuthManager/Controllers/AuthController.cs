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
using WebApi.Dtos.Auth;

namespace Controllers.MenuManager.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class AuthController : ControllerBase
    {
        AuthService _service;
        Lazy<ILogger> _localLogger;
        public AuthController(IAuthServices services, ILoggerFactory loggerFactory)
        {
            _localLogger = new Lazy<ILogger>(() => loggerFactory.CreateLogger(GetType()));
            _service = services as AuthService;
        }
        [HttpPost]
        public IActionResult Login([FromBody] UserDto user)
        {
            // 模拟用户验证（可替换为数据库验证）
            if (user.username == "admin" && user.password == "111111")
            {
                // 用户验证通过，生成 JWT Token
                var token = _service.CreateToken(user.username);
                return Ok(new { Token = token });
            }
            // 验证失败
            return Unauthorized(new { Message = "Invalid username or password." });
        }
    }
}
