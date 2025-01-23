using System;
using System.Security.Cryptography;

namespace WebApi.Utilities.Extensions
{
    public static class String
    {
        static string privatekey = "<RSAKeyValue><Modulus>wwVIv9PAgyifmUff0mWLQ2zUyVeo8j5Gin6tWqKgBl9Whg4ZIJIdRbOjAqHkQ7KVCGeXfUBbt3Qyhhlk3RuhyhR3JySmfHmRfcSEQ+/zaSrIotefGYbufHPOPr7qIf+Xo1UKvVlDd0e4dlic6Ftwpd+8a0TBAD21jByYvYnNPOs=</Modulus><Exponent>AQAB</Exponent><P>/ilke55nf8j001sFckfWeY7VeHQ+7uOGv/8Xc7KsGLYh784mYWb67d1mDW+QukJ8sLq+vsR0F6ubrfJkaV2EZw==</P><Q>xG5ivYVJuUT/JxPSKZwIFVfIrEU6jSczPQw9SbZVfihh/QzEYxo4wFBF9gr1UiP/jtdj38f36XgiUuPeeqiQ3Q==</Q><DP>fLKeRDBxozZbOB9eSrWIOtejUJfoEJi9EhH71Z4B5ZXmjJteJUe7MV0ApvLn1Rqtxp+42ivUsZBWrz9PmIpzXQ==</DP><DQ>kIrty6nl+xfjbXzvXED9zb6/4sw6bp3W3WhBPRmXxXKv0EZxyL8F3bX0FT4xERV+Oz0RuBzst3b4Quh80ONzXQ==</DQ><InverseQ>/d5T4J1ktI9IQsS11o01kqHlZ7jNc2DrtZePcZ83KP+vhf/MMkPguYb904SRjGZ+hx0AkEkKC3rirUzLzqpseQ==</InverseQ><D>g9iHtYCgT0vgko1gCWszOrNSLIkCJDvsDufTpUn67DP6WAkh/b4q4huvYjE3FHmgfh0i8r/XAfEh0z1Jzp5Gwxpu7PdY5rbH9xjTYPh3xVrb9BdKXOff0XHfFrtLpDCIr24Nns2JhJBCn8XsyA/tq/SngZQ0Sd+lJ3XnBmobeXk=</D></RSAKeyValue>";//私钥 
        static string publickey = "<RSAKeyValue><Modulus>wwVIv9PAgyifmUff0mWLQ2zUyVeo8j5Gin6tWqKgBl9Whg4ZIJIdRbOjAqHkQ7KVCGeXfUBbt3Qyhhlk3RuhyhR3JySmfHmRfcSEQ+/zaSrIotefGYbufHPOPr7qIf+Xo1UKvVlDd0e4dlic6Ftwpd+8a0TBAD21jByYvYnNPOs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";//公钥 

        public static string PublicKey { get => publickey; }
        public static string PrivateKey { get => privatekey; }

        /// <summary>
        /// HEX字符串转换为BYTES
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToHexBytes(this string str)
        {
            if ((str.Length % 2) != 0) str += " ";

            byte[] returnBytes = new byte[str.Length / 2];

            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);

            return returnBytes;
        }

        /// <summary>
        /// 转字符串转BCD码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToBcd(this string str)
        {
            try
            {
                if (Convert.ToBoolean(str.Length & 1))//数字的二进制码最后1位是1则为奇数
                {
                    str = str + "0";//数位为奇数时后面补0
                }

                byte[] aryTemp = new byte[str.Length / 2];

                for (int i = 0; i < (str.Length / 2); i++)
                {
                    aryTemp[i] = (Byte)(((str[i * 2] - '0') << 4) | (str[i * 2 + 1] - '0'));
                }
                return aryTemp;//高位在前
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// HEX字符串转换为BYTE
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte ToHexByte(this string str)
        {
            if (str.Length < 2) str = "00";
            return Convert.ToByte(str.Substring(0, 2), 16);
        }

        /// <summary>
        /// STRING字符串转换为SHA256
        /// UTF-8编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToSha256(this string str)
        {
            return SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// 字符串转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToAsciiBytes(this string str)
        {
            return System.Text.Encoding.ASCII.GetBytes(str);
        }
    }
}
