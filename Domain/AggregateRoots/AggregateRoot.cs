using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.AggregateRoots
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        #region Object Member

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            IAggregateRoot? ar = obj as IAggregateRoot;
            if (ar == null)
                return false;
            return this.Key == ar.Key;
        }

        #endregion
    }
}
