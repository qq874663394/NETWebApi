using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Filters
{
    /// <summary>
    /// 分页特性
    /// </summary>
    public class HeadersResultFilterAttribute : ResultFilterAttribute, IResultFilter
    {
        string _key = string.Empty;
        string _value = string.Empty;

        /// <summary>
        /// [HeadersResultFilterAttribute("Access-Control-Expose-Headers", "TotalPages,TotalRecords")]
        /// Response.Headers.Add("TotalPages", data.TotalPages.ToString());
        /// Response.Headers.Add("TotalRecords", data.TotalRecords.ToString());
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public HeadersResultFilterAttribute(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.HttpContext.Response.Headers.ContainsKey(_key))
            {
                context.HttpContext.Response.Headers.Add(_key, _value);
            }

            base.OnResultExecuting(context);
        }
    }
}
