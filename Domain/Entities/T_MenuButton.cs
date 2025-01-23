using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;

using WebApi.Domain.Interface.IAggregateRoots;
namespace WebApi.Domain.Entities
{
    public partial class T_MenuButton : Entity, IAggregateRoot
    {
        public Guid? MenuCode { get; set; }
        public Guid? ButtonCode { get; set; }
        public int? SortOrder { get; set; }

        public virtual T_Button? Button { get; set; }
    }
}
