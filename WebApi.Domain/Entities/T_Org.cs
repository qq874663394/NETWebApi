using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities
{
    public partial class T_Org : Entity, IAggregateRoot
    {
        public T_Org()
        {
            T_UserOrgs = new HashSet<T_UserOrg>();
        }
        public Guid? ParentCode { get; set; }
        public string? NodeType { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<T_UserOrg> T_UserOrgs { get; set; }
    }
}
