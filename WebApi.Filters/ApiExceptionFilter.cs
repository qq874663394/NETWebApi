using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using WebApi.DependencyInjection;
using WebApi.Domain.Exceptions;
using WebApi.Utilities.ConstValue;

namespace WebApi.Filters
{
    //public class ApiExceptionFilter : IExceptionFilter
    //{
        //#region 日志
        //private static ILogger _logger = IocFactory.GetService<ILoggerFactory>().CreateLogger<ApiExceptionFilter>();
        //#endregion

        //public void OnException(ExceptionContext context)
        //{
        //    _logger.LogError(context.Exception.HResult, context.Exception, "应用程序出现异常");

        //    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        //    Error err = new Error()
        //    {
        //        StatusCode = HttpStatusCode.InternalServerError,
        //        Message = "应用程序出现异常"
        //    };
        //    err.Errors.Add(new Error.Err() { Code = context.Exception.HResult, Message = "应用程序出现异常", Type = API_ERROR_TYPE.EXCEPTION, Detils = context.Exception.Message });

        //    context.Result = new ObjectResult(err);
        //}
    //}
}