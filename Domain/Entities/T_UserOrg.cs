using System;
using System.Collections.Generic;
using Domain.AggregateRoots;
using Domain.Interface.IAggregateRoots;

namespace Domain.Entities
{
    /// <summary>
    /// 用户所属组织架构关系表
    /// </summary>
    public partial class T_UserOrg : Entity, IAggregateRoot
    {
        public Guid? UserCode { get; set; }
        public Guid? OrgCode { get; set; }

        public virtual T_Org? Org { get; set; }
        public virtual T_User? User { get; set; }
    }
}
