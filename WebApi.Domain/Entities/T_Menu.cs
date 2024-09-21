using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;

using WebApi.Domain.Interface.IAggregateRoots;
namespace WebApi.Domain.Entities
{
    public partial class T_Menu : Entity, IAggregateRoot
    {
        public T_Menu()
        {
            T_Buttons = new HashSet<T_Button>();
        }

        public string? Name { get; set; }
        public Guid? ParentCode { get; set; }
        public string? URL { get; set; }
        public string? Icon { get; set; }
        public int? SortOrder { get; set; }
        public int? IsVisible { get; set; }

        public virtual ICollection<T_Button> T_Buttons { get; set; }
    }
}
