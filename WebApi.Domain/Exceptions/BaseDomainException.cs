using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain.Exceptions
{
    /// <summary>
    /// 业务逻辑层处理抛出的异常类型
    /// </summary>
    [Serializable]
    public class BaseDomainException : Exception
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int Code { get; private set; }

        /// <summary>
        /// 错误类型
        /// </summary>
        public string API_ERROR_TYPE { get; private set; }

        /// <summary>
        /// 详细信息
        /// </summary>
        public string Detail { get; private set; }

        /// <summary>  
        /// 默认构造函数  
        /// </summary>  
        public BaseDomainException()
        {
        }
        public BaseDomainException(string message)
            : base(message)
        {
        }
        public BaseDomainException(string message, System.Exception inner)
            : base(message, inner)
        {

        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="message"></param>
        /// <param name="detail"></param>
        /// <param name="recive"></param>
        /// <param name="remoteIP"></param>
        public BaseDomainException(int code, string type, string message, string detail) :
            this(message)
        {
            Code = code;
            API_ERROR_TYPE = type;
            Detail = detail;
        }
    }
}
