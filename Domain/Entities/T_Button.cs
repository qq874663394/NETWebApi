using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities
{
    public partial class T_Button : Entity,IAggregateRoot
    {
        public T_Button()
        {
            T_ButtonPermissions = new HashSet<T_ButtonPermission>();
            T_MenuButtons = new HashSet<T_MenuButton>();
        }

        public Guid? MenuCode { get; set; }
        public string? Name { get; set; }
        public string? Action { get; set; }
        public string? Icon { get; set; }
        public int? SortOrder { get; set; }
        public int? IsVisible { get; set; }

        public virtual T_Menu? Menu { get; set; }
        public virtual ICollection<T_ButtonPermission> T_ButtonPermissions { get; set; }
        public virtual ICollection<T_MenuButton> T_MenuButtons { get; set; }
    }
}
