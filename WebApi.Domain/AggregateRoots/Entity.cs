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
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.AggregateRoots
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
        [Browsable(false)]
        [Key]
        public virtual Guid Code { get; set; }
        public virtual DateTime? CreateTime { get; set; }
        public virtual Guid? CreateUserCode { get; set; }
        public virtual DateTime? ModifyTime { get; set; }
        public virtual Guid? ModifyUserCode { get; set; }
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
