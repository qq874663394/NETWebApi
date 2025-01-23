using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Model;

namespace WebApi.Domain.Interface.IServices
{
    public interface IAuthServices
    {
        /// <summary>
        /// 验证Token并获取数据
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JwtToken ValidateToken(string token);
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="roleId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public string CreateToken(string userId);
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string Refresh(string token);
        /// <summary>
        /// 获取JwtPayload数据
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        public JwtPayload GetJwtPayload(string jwtToken);
        /// <summary>
        /// 秘钥转128位秘钥
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public byte[] ConvertToByteArray(string input);
    }
}
