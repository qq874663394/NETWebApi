using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Domain.Interface.IAggregateRoots
{
    public interface IEntity
    {
        [JsonIgnore]
        string Key { get; }
        [JsonIgnore]
        string KeyName { get; }

        abstract bool Equals(object obj);
        /// <summary>
        /// 判断主键是否为空，常用做判定操作是【添加】还是【编辑】
        /// </summary>
        /// <returns></returns>
        public bool KeyIsNull();

        /// <summary>
        /// 创建默认的主键值
        /// </summary>
        public void GenerateDefaultKeyVal();
    }
}
