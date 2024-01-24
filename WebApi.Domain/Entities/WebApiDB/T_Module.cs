using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Domain.AggregateRoot;
using WebApi.Domain.Interface.IAggregateRoots.TreeEntity;
using System.ComponentModel;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities.WebApiDB
{
    /// <summary>
    /// 功能模块表
    /// </summary>
    public partial class T_Module : Entity, IAggregateRoot
    {
        public override string Key { get => Key; set => Key = value; }

        public override string KeyName => "Id";
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; } = null!;
        /// <summary>
        /// 主页面URL
        /// </summary>
        [Description("主页面URL")]
        public string Url { get; set; }
        /// <summary>
        /// 热键
        /// </summary>
        [Description("热键")]
        public string HotKey { get; set; }
        /// <summary>
        /// 是否叶子节点
        /// </summary>
        [Description("是否叶子节点")]
        public bool IsLeaf { get; set; }
        /// <summary>
        /// 是否自动展开
        /// </summary>
        [Description("是否自动展开")]
        public bool IsAutoExpand { get; set; }
        /// <summary>
        /// 节点图标文件名称
        /// </summary>
        [Description("节点图标文件名称")]
        public string IconName { get; set; }
        /// <summary>
        /// 当前状态，0：正常，-1：隐藏，不在导航列表中显示
        /// </summary>
        [Description("当前状态")]
        public int Status { get; set; }

        /// <summary>
        /// 矢量图标
        /// </summary>
        [Description("矢量图标")]
        public string Vector { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        [Description("排序号")]
        public int SortNo { get; set; }

        /// <summary>
        /// 模块标识
        /// </summary>
        [Description("模块标识")]
        public string Code { get; set; }

        /// <summary>
        /// 是否系统模块
        /// </summary>
        [Description("是否系统模块")]
        public bool IsSys { get; set; }
        public string CascadeId { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
    }
}
