using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Interface.IServices;
using WebApi.Domain.Model;

namespace WebApi.Domain.Services
{
    public class AuthService : IAuthServices
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 注入appsetting
        /// </summary>
        /// <param name="configuration"></param>
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = ConvertToByteArray(_configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", userId),
                 }),
                //"Issuer": "https://localhost:7141", //颁发者
                //"Audience": "https://localhost:7141", //使用者
                //"Expirces": 3600, //Token过期时间，
                //"RefreshTokenExpirces": 3600 //refresh_Token过期时间
                Expires = DateTime.UtcNow.AddSeconds(Convert.ToInt32(_configuration["Jwt:Expirces"])),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public JwtPayload GetJwtPayload(string jwtToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // 解析JWT token
            if (tokenHandler.CanReadToken(jwtToken))
            {
                JwtSecurityToken parsedToken = tokenHandler.ReadJwtToken(jwtToken);
                return parsedToken.Payload;
            }
            else
            {
                throw new SecurityTokenException("Invalid JWT token");
            }
        }

        public string Refresh(string token)
        {
            List<Claim> list_JwtPayload = GetJwtPayload(token).Claims.ToList();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = ConvertToByteArray(_configuration["Jwt:SecretKey"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                RequireExpirationTime = true,
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtToken = securityToken as JwtSecurityToken;

            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            // 检查令牌是否已经过期
            var expires = jwtToken.ValidTo;
            if (expires > DateTime.UtcNow)
            {
                throw new SecurityTokenException("Token has not expired yet");
            }
            string newToken = "";
            // 生成新的JWT令牌
            //var newToken = GenerateToken(principal.Identity.Name); // 根据需要自行实现生成新令牌的逻辑
            return newToken;
        }
        public byte[] ConvertToByteArray(string input)
        {
            // 使用SHA-256哈希算法计算输入字符串的哈希值
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                return sha256.ComputeHash(bytes);
            }
        }
        public JwtToken ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = ConvertToByteArray(_configuration["Jwt:SecretKey"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,//是否验证过期时间，过期了就拒绝访问
                    RequireExpirationTime = true,
                }, out SecurityToken validatedToken);
                var jwtSecurityToken = validatedToken as JwtSecurityToken;
                JwtToken jwtToken = new JwtToken()
                {
                    UserId = Guid.Parse(jwtSecurityToken.Claims.FirstOrDefault(p => p.Type == "userId").Value.ToString())
                };
                return jwtToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}