using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApi.Domain.AggregateRoot;

namespace WebApi.Domain.Entities.WebApiDB
{
    public partial class T_Role : Entity
    {
        public override string Key { get => Key; set => Key = value; }

        public override string KeyName => "Id";
        [Key]
        public string Id { get; set; } = null!;

        /// <summary>
        /// 角色名称
        /// </summary>
        [Description("角色名称")]
        public string Name { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        [Description("当前状态")]
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        [Description("创建人ID")]
        public string CreateId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [Description("分类名称")]
        public string TypeName { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        [Description("分类ID")]
        public string TypeId { get; set; }
    }
}