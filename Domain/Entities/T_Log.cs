using System;
using System.Collections.Generic;
using Domain.AggregateRoots;
using Domain.Interface.IAggregateRoots;

namespace Domain.Entities
{
    public partial class T_Log : Entity, IAggregateRoot
    {
        public Guid? UserCode { get; set; }
        public string? Action { get; set; }
        public string? Description { get; set; }
        public string? IP { get; set; }

        public virtual T_User? User { get; set; }
    }
}
