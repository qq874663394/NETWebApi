using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Exceptions;
using WebApi.Utilities.ConstValue;
using WebApi.Utilities.Extensions;

namespace WebApi.Filters
{

    /// <summary>
    /// 签名验证
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
            Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SignAuthAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private string _customerId;
        private AuthorizationFilterContext _context;

        public SignAuthAttribute()
        {
            //_customerId = SysParameterManager.Instance.CustomerId;
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");

            if (context.HttpContext.Request.Headers.TryGetValue("Sign", out StringValues sign))
            {
                if (context.HttpContext.Request.Headers.TryGetValue("Nonce", out StringValues nonce))
                {
                    if (System.Text.Encoding.UTF8.GetBytes($"id={_customerId}&nonce={nonce[0]}").GetMD5().ToHexStr().Equals(sign[0]))
                    {
                        return;
                    }
                }
            }

            Error err = new Error()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "接口权限验证失败"
            };
            err.Errors.Add(new Error.Err()
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = "无效的签名数据",
                Type = API_ERROR_TYPE.INVALID_SIGN,
                Detils = "签名数据验证失败！"
            });

            _context.Result = new ObjectResult(err);
        }
    }
}
