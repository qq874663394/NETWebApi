using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Utilities
{
    public class AppSetting
    {
        /// <summary>
        /// 数据库类型 SqlServer、MySql
        /// </summary>
        public Dictionary<string, string> DbTypes { get; set; }
        public string DbConnections { get; set; }
    }
}
