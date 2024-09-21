using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities
{
    public partial class T_User : Entity, IAggregateRoot
    {
        public T_User()
        {
            T_Emails = new HashSet<T_Email>();
            T_Logs = new HashSet<T_Log>();
            T_UserOrgs = new HashSet<T_UserOrg>();
            T_UserRoles = new HashSet<T_UserRole>();
            T_Role = new HashSet<T_Role>();
            T_Org = new HashSet<T_Org>();
        }

        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public int? Sex { get; set; }
        public string? Email { get; set; }
        public int? Enable { get; set; }

        public virtual ICollection<T_Email> T_Emails { get; set; }
        public virtual ICollection<T_Log> T_Logs { get; set; }
        public virtual ICollection<T_UserOrg> T_UserOrgs { get; set; }
        public virtual ICollection<T_UserRole> T_UserRoles { get; set; }
        public virtual ICollection<T_Role> T_Role { get; set; }
        public virtual ICollection<T_Org> T_Org { get; set; }

    }
}
