using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace WebApi.Domain.Interface.IAggregateRoots
{
    public interface IEntity
    {
        string Key { get; set; }
        string KeyName { get; }

        #region 通用属性
        Guid Id { get; set; }
        DateTime? CreateTime { get; set; }
        Guid? CreateUserId { get; set; }
        DateTime? ModifyTime { get; set; }
        Guid? ModifyUserId { get; set; }
        #endregion

        abstract bool Equals(object obj);
        /// <summary>
        /// 判断主键是否为空，常用做判定操作是【添加】还是【编辑】
        /// </summary>
        /// <returns></returns>
        abstract bool KeyIsNull();

        /// <summary>
        /// 创建默认的主键值
        /// </summary>
        abstract void GenerateDefaultKeyVal();
    }
}
