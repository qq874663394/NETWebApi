
namespace WebApi.Utilities.Extensions
{
    public static class MyByte
    {
        #region 转字符串
        /// <summary>
        /// BYTES转换为HEX字符串
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <returns></returns>
        public static string ToHexStr(this byte value)
        {
            return value.ToString("X2");
        }
        #endregion
    }
}
