using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities
{
    public partial class T_Role : Entity, IAggregateRoot
    {
        public T_Role()
        {
            T_UserRoles = new HashSet<T_UserRole>();
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string? Description { get; set; }

        public virtual ICollection<T_UserRole> T_UserRoles { get; set; }
    }
}
