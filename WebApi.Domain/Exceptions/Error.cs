using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.AggregateRoot;

namespace WebApi.Domain.Exceptions
{
    public class Error : Entity
    {
        public override string Key { get => Guid.NewGuid().ToString(); set => GenerateDefaultKeyVal(); }

        public override string KeyName => "Key";

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;
        public string Message { get; set; } = string.Empty;
        public List<Err> Errors { get; set; } = new List<Err>();

        public static Error Create(HttpStatusCode status, string err, int code, string type, string msg, string detail)
        {
            Error error = new Error()
            {
                StatusCode = status,
                Message = err
            };
            error.Errors.Add(new Error.Err()
            {
                Code = code,
                Message = msg,
                Type = type,
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
