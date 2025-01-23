using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities
{
    public partial class T_UserRole : Entity, IAggregateRoot
    {
        public Guid? UserCode { get; set; }
        public Guid? RoleCode { get; set; }

        public virtual T_Role? Role { get; set; }
        public virtual T_User? User { get; set; }
    }
}
