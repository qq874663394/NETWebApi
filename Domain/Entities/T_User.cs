using System;
using System.Collections.Generic;
using Domain.AggregateRoots;
using Domain.Interface.IAggregateRoots;

namespace Domain.Entities
{
    /// <summary>
    /// 用户信息表
    /// </summary>
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
        /// <summary>
        /// 用户名
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 用户APO，
        /// </summary>
        public string? Apo { get; set; }
        /// <summary>
        /// 用户全名
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; } = string.Empty;
        /// <summary>
        /// 座机号
        /// </summary>
        public string Tel { get; set; } = string.Empty;
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public int DocumentType { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string? DocumentNumber { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 是否活动
        /// </summary>
        public bool IsActive { get; set; } = true;


        public virtual ICollection<T_Email> T_Emails { get; set; }
        public virtual ICollection<T_Log> T_Logs { get; set; }
        /// <summary>
        /// 用户部门
        /// </summary>
        public virtual ICollection<T_UserOrg> T_UserOrgs { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public virtual ICollection<T_UserRole> T_UserRoles { get; set; }
        public virtual ICollection<T_Role> T_Role { get; set; }
        public virtual ICollection<T_Org> T_Org { get; set; }

    }
}
