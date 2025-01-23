using System.Collections.Generic;
using System.Text;

namespace WebApi.Utilities.Extensions
{
    public static class List
    {
        /// <summary>
        /// 使用指定的分隔符分割list
        /// </summary>
        /// <returns></returns>
        public static string SplitToString(this List<string> list, char split)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in list)
            {
                if (sb.Length > 0)
                    sb.Append(' ').Append(split).Append(' ');

                sb.Append(item);
            }

            return sb.ToString();
        }
    }
}
