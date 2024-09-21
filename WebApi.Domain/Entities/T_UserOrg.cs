﻿using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities
{
    public partial class T_UserOrg : Entity, IAggregateRoot
    {
        public Guid? UserCode { get; set; }
        public Guid? OrgCode { get; set; }

        public virtual T_Org? Org { get; set; }
        public virtual T_User? User { get; set; }
    }
}
