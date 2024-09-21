using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities
{
    public partial class T_ButtonPermission : Entity, IAggregateRoot
    {
        public Guid? OtherCode { get; set; }
        public Guid? ButtonCode { get; set; }
        public string? Type { get; set; }
        public int? Enable { get; set; }

        public virtual T_Button? Button { get; set; }
    }
}
