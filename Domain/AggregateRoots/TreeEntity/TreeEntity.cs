using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.AggregateRoots.TreeEntity
{
    /// <summary>
    /// 树状结构实体
    /// </summary>
    public abstract class TreeEntity : Entity
    {
        /// <summary>
        /// 节点语义ID
        /// </summary>
        [Description("节点语义ID")]
        public string? CascadeId { get; set; }
        /// <summary>
        /// 功能模块名称
        /// </summary>
        [Description("名称")]
        public string? Name { get; set; }

        /// <summary>
        /// 父节点流水号
        /// </summary>
        [Description("父节点流水号")]
        public string ParentId { get; set; }
        /// <summary>
        /// 父节点名称
        /// </summary>
        [Description("父节点名称")]
        public string? ParentName { get; set; }

    }
}
