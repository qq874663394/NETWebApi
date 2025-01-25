using System;
using System.Collections.Generic;
using Domain.AggregateRoots;
using Domain.Interface.IAggregateRoots;

namespace Domain.Entities
{
    public partial class T_Resource : Entity, IAggregateRoot
    {
        public string? Name { get; set; }
        public Guid? ParentCode { get; set; }
        public string? Type { get; set; }
        public string? Path { get; set; }
        public string? Description { get; set; }
    }
}
