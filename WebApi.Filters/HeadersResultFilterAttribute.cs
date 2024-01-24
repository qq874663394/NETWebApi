using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Filters
{

    public class HeadersResultFilterAttribute : ResultFilterAttribute, IResultFilter
    {
        string _key = string.Empty;
        string _value = string.Empty;

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
