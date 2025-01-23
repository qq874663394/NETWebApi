using System;
using System.Collections.Generic;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities
{
    public partial class T_Email : Entity, IAggregateRoot
    {
        public Guid? SendUserCode { get; set; }
        public string? Recipient { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public int? IsSend { get; set; }
        public DateTime? SentTime { get; set; }

        public virtual T_User? SendUser { get; set; }
    }
}
