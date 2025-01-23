using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
namespace WebApi.Utilities.Extensions
{
    public static class Bytes
    {
        #region 转字符串
        /// <summary>
        /// BYTES转换为HEX字符串
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <returns></returns>
        public static string ToHexStr(this byte[] bytes)
        {
            string returnStr = "";

            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }

            return returnStr;
        }

        /// <summary>
        /// 从BYTES指定位置转换指定长度为HEX字符串
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <param name="offset">偏移位</param>
        /// <param name="count">长度</param>
        /// <returns></returns>
        public static string ToHexStr(this byte[] bytes, int offset, int count)
        {
            string returnStr = "";

            if (bytes != null)
            {
                for (int i = offset; i < offset + count; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }

            return returnStr;
        }

        /// <summary>
        /// 指定长度BYTES转换为HEX字符串
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string ToHexStr(this byte[] bytes, int length)
        {
            string returnStr = "";

            if (bytes != null)
            {
                for (int i = 0; i < length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }

            return returnStr;
        }
        #endregion

        #region 判断全FF
        /// <summary>
        /// 判断是否全为FF
        /// </summary>
        /// <returns></returns>
        public static bool IsAllFF(this byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
                if (bytes[i] != 0xff)
                    return false;
            return true;
        }

        /// <summary>
        /// 判断是否全为FF
        /// </summary>
        /// <param name="start">/param>
        /// <param name="length">/param>
        /// <returns></returns>
        public static bool IsAllFF(this byte[] bytes, int start, int length)
        {
            for (int i = start; i < start + length; i++)
                if (bytes[i] != 0xff)
                    return false;
            return true;
        }
        #endregion

        #region 异或
        /// <summary>
        /// 异或
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <param name="obj">异或对象</param>
        /// <returns></returns>
        public static byte[] Xor(this byte[] bytes, byte[] obj)
        {
            if (bytes.Length != obj.Length) throw new Exception("长度不一致");

            MemoryStream ms = new MemoryStream();

            for (int i = 0; i < bytes.Length; i++)
            {
                int xor = bytes[i] ^ obj[i];
                ms.WriteByte(BitConverter.GetBytes(xor)[0]);
            }

            return ms.ToArray();
        }
        #endregion

        #region GZIP压缩
        /// <summary>
        /// GZIP压缩数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GZipCompress(this byte[] data)
        {
            byte[] ret = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(ms,
                    System.IO.Compression.CompressionMode.Compress))
                {
                    //将数据写入基础流，同时会被压缩
                    gzip.Write(data, 0, data.Length);
                }
                ret = ms.ToArray();
            }
            return ret;
        }

        /// <summary>
        /// GZIP解压缩数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GZipDecompress(this byte[] data)
        {
            byte[] ret = null;

            using (MemoryStream cms = new MemoryStream(data))
            {
                using (MemoryStream dms = new MemoryStream())
                {
                    using (System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(cms,
                    System.IO.Compression.CompressionMode.Decompress))
                    {
                        byte[] bytes = new byte[1024];
                        int len = 0;
                        //读取压缩流，同时会被解压
                        while ((len = gzip.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            dms.Write(bytes, 0, len);
                        }

                        ret = dms.ToArray();
                    }
                }
            }

            return ret;
        }
        #endregion

        #region DES加解密
        //默认向量
        private static byte[] _iv_des = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        //默认密钥
        private static byte[] _key_des = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

        /// <summary>
        /// Des加密
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        public static byte[] EncByDes(this byte[] data, byte[] key = null, byte[] iv = null)
        {
            if (iv == null) iv = _iv_des;
            if (key == null) key = _key_des;

            byte[] ret = null;

            var DCSP = Aes.Create();
            DCSP.Mode = CipherMode.CBC;
            DCSP.Padding = PaddingMode.PKCS7;

            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    cStream.Write(data, 0, data.Length);
                    cStream.FlushFinalBlock();
                }

                ret = mStream.ToArray();
            }

            return ret;
        }

        /// <summary>
        /// Des解密
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        public static byte[] DecByDes(this byte[] data, byte[] key = null, byte[] iv = null)
        {
            if (iv == null) iv = _iv_des;
            if (key == null) key = _key_des;

            byte[] ret = null;

            var DCSP = Aes.Create();
            DCSP.Mode = CipherMode.CBC;
            DCSP.Padding = PaddingMode.PKCS7;

            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(key, iv), CryptoStreamMode.Write))
                {
                    cStream.Write(data, 0, data.Length);
                    cStream.FlushFinalBlock();
                }

                ret = mStream.ToArray();
            }

            return ret;
        }
        #endregion

        #region AES加解密
        //默认向量
        private static byte[] _iv_Aes = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        //默认密钥
        private static byte[] _key_Aes = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

        /// <summary>
        /// Des加密
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        public static byte[] EncByAes(this byte[] data, byte[] key = null, byte[] iv = null)
        {
            if (iv == null) iv = _iv_des;
            if (key == null) key = _key_des;

            byte[] ret = null;

            var DCSP = Aes.Create();
            DCSP.Mode = CipherMode.CBC;
            DCSP.Padding = PaddingMode.PKCS7;

            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    cStream.Write(data, 0, data.Length);
                    cStream.FlushFinalBlock();
                }

                ret = mStream.ToArray();
            }

            return ret;
        }

        /// <summary>
        /// Des解密
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        public static byte[] DecByAes(this byte[] data, byte[] key = null, byte[] iv = null)
        {
            if (iv == null) iv = _iv_Aes;
            if (key == null) key = _key_Aes;

            byte[] ret = null;

            var DCSP = Aes.Create();
            DCSP.Mode = CipherMode.CBC;
            DCSP.Padding = PaddingMode.PKCS7;

            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(key, iv), CryptoStreamMode.Write))
                {
                    cStream.Write(data, 0, data.Length);
                    cStream.FlushFinalBlock();
                }

                ret = mStream.ToArray();
            }

            return ret;
        }
        #endregion

        #region MD5
        /// <summary>
        /// 计算数据的MD5值
        /// </summary>
        /// <param name="data">需要计算的数据</param>
        /// <returns></returns>
        public static byte[] GetMD5(this byte[] data)
        {
            return MD5.Create().ComputeHash(data);
        }
        #endregion

        #region RSA
        /// <summary>
        /// 非对称加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] RSAEncrypt(this byte[] content, string publicKey)
        {
            var rsa = RSA.Create();

            RSAParameters parameters = new RSAParameters();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(publicKey);
            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
                        case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
                        case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
                        case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
                        case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
                        case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
                        case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);

            return rsa.Encrypt(content, RSAEncryptionPadding.Pkcs1);
        }

        /// <summary>
        /// 非对称解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] RSADecrypt(this byte[] content, string privateKey)
        {
            var rsa = RSA.Create();

            RSAParameters parameters = new RSAParameters();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(privateKey);
            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
                        case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
                        case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
                        case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
                        case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
                        case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
                        case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);

            return rsa.Decrypt(content, RSAEncryptionPadding.Pkcs1);
        }
        #endregion
    }
}
