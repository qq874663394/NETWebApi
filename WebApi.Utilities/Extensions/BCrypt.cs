using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Utilities.Extensions
{
    /// <summary>
    /// 提供了对 BCrypt 哈希算法的简单封装。
    /// </summary>
    public class CryptHelper
    {
        /// <summary>
        /// 使用 BCrypt 算法对密码进行哈希。
        /// </summary>
        /// <param name="password">要哈希的密码。</param>
        /// <returns>哈希后的密码。</returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// 验证密码与哈希密码是否匹配。
        /// </summary>
        /// <param name="password">要验证的密码。</param>
        /// <param name="hashedPassword">已哈希的密码。</param>
        /// <returns>如果密码匹配，则为 true；否则为 false。</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
