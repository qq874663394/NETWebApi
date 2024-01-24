using System.Collections.Generic;
using System.Text;

namespace WebApi.Utilities.Extensions
{
    public static class Dictionary
    {
        /// <summary>
        /// 格式化序列
        /// </summary>
        /// <param name="In"></param>
        /// <returns></returns>
        public static string Format(this Dictionary<string, string> dictionary)
        {
            if (dictionary == null || dictionary.Count < 1)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder(300);
            sb.Append("{");

            foreach (var item in dictionary)
            {
                if (sb.Length != 1)
                {
                    sb.Append(",");
                }

                sb.Append("\"" + item.Key + "\":\"" + item.Value + "\"");
            }

            sb.Append("}");

            return sb.ToString();
        }
    }
}
