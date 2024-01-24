using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApi.Domain.AggregateRoot;

namespace WebApi.Domain.Entities.WebApiDB
{
    public partial class T_ModuleElement : Entity
    {
        public override string Key { get => Key; set => Key = value; }

        public override string KeyName => "Id";
        [Key]
        public string Id { get; set; } = null!;
        /// <summary>
        /// DOM ID
        /// </summary>
        [Description("DOM ID")]
        public string DomId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 元素附加属性
        /// </summary>
        [Description("元素附加属性")]
        public string Attr { get; set; }
        /// <summary>
        /// 元素调用脚本
        /// </summary>
        [Description("元素调用脚本")]
        public string Script { get; set; }
        /// <summary>
        /// 元素图标
        /// </summary>
        [Description("元素图标")]
        public string Icon { get; set; }
        /// <summary>
        /// 元素样式
        /// </summary>
        [Description("元素样式")]
        public string Class { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Description("备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        [Description("排序字段")]
        public int Sort { get; set; }
        /// <summary>
        /// 功能模块Id
        /// </summary>
        [Description("功能模块Id")]
        public string ModuleId { get; set; }
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
