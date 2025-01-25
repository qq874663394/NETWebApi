using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Interface.IAggregateRoots;

namespace Domain.AggregateRoots
{
    public abstract class Entity : IEntity
    {
        [JsonIgnore]
        [NotMapped]
        public string Key { get => Code.ToString(); set => value = Code.ToString(); }
        [JsonIgnore]
        [NotMapped]
        public string KeyName { get => "Code"; set => value = "Code"; }

        #region 通用属性
        /// <summary>
        /// 标识列
        /// </summary>
        [Browsable(false)]
        [Key]
        public virtual Guid Code { get; set; }
        public virtual DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建用户Code
        /// </summary>
        public virtual Guid? CreateUserCode { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 修改用户Code
        /// </summary>
        public virtual Guid? ModifyUserCode { get; set; }
        /// <summary>
        /// 是否启用，false否，true是，默认true
        /// </summary>
        public bool IsEnable { get; set; } = true;
        /// <summary>
        /// 是否删除，false否，true是，默认false
        /// </summary>
        public bool IsDelete { get; set; } = false;
        #endregion

        public bool KeyIsNull()
        {
            return string.IsNullOrEmpty(Key);
        }

        /// <summary>
        /// 创建默认的主键值
        /// </summary>
        public virtual void GenerateDefaultKeyVal()
        {
            Key = Guid.NewGuid().ToString();
        }
    }
}
