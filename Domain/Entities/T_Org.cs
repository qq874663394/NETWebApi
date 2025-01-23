using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities
{
    /// <summary>
    /// 组织架构表
    /// </summary>
    public partial class T_Org : Entity, IAggregateRoot
    {
        public T_Org()
        {
            T_UserOrgs = new HashSet<T_UserOrg>();
        }
        /// <summary>
        /// 父级Code
        /// </summary>
        public Guid? ParentCode { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public string? NodeType { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 用户、组织架构关系
        /// </summary>
        public virtual ICollection<T_UserOrg> T_UserOrgs { get; set; }
    }
}
