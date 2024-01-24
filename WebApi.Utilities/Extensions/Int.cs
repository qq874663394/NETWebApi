using System;

namespace WebApi.Utilities.Extensions
{
    public static class Int
    {
        /// <summary>
        /// 转换为UInt16
        /// </summary>
        /// <param name="In"></param>
        /// <returns></returns>
        public static ushort ToUInt16(this int integer)
        {
            if (integer >= 0 && integer <= 65535)
            {
                return Convert.ToUInt16(integer);
            }
            else
            {
                throw new OverflowException("尝试将Int32转换为UInt16时，原始值超出了0~65535的范围！");
            }
        }
    }
}
