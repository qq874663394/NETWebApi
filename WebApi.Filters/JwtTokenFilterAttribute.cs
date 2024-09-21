using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.DependencyInjection;
using WebApi.Domain.Interface.IServices;
using WebApi.Domain.Services;

namespace WebApi.Filters
{

    /// <summary>
    /// 签名验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class JwtTokenFilterAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private readonly IAuthServices _authServices;
        public JwtTokenFilterAttribute(IAuthServices authServices)
        {
            _authServices = authServices;
        }
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {

            // 从请求头中获取 JWT Token
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                // 解析 JWT Token
                var jwtToken = _authServices.ValidateToken(token);

                if (jwtToken != null)
                {
                    // 将 JWT Token 中的信息存储在 HttpContext 中，以便后续使用
                    context.HttpContext.Items["JwtToken"] = jwtToken;
                }
            }
        }
    }
}
