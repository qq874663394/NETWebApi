using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.AggregateRoot
{
    public abstract class Entity : IEntity
    {
        [JsonIgnore]
        public abstract string Key { get; set; }
        [JsonIgnore]
        public abstract string KeyName { get; }

        /// <summary>
        /// 判断主键是否为空，常用做判定操作是【添加】还是【编辑】
        /// </summary>
        /// <returns></returns>
        public bool KeyIsNull()
        {
            return string.IsNullOrEmpty(Key);
        }

        /// <summary>
        /// 创建默认的主键值
        /// </summary>
        public void GenerateDefaultKeyVal()
        {
            Key = Guid.NewGuid().ToString();
        }
    }
}
