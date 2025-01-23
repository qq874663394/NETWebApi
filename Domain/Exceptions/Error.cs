using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.AggregateRoots;

namespace WebApi.Domain.Exceptions
{
    public class Error : Entity
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;
        public string Message { get; set; } = string.Empty;
        public List<Err> Errors { get; set; } = new List<Err>();

        public static Error Create(HttpStatusCode status, string msg, string detail = "")
        {
            Error error = new Error()
            {
                StatusCode = status,
                Message = msg
            };
            error.Errors.Add(new Error.Err()
            {
                Code = (int)status,
                Message = msg,
                Detils = detail
            });
            return error;
        }

        public class Err
        {
            public int Code { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
            public string Detils { get; set; }
        }
    }
}
