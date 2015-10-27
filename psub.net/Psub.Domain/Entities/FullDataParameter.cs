using System;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class FullDataParameter : Base
    {
        public virtual string Value { get; set; }
        public virtual ControlObject ControlObject { get; set; }
        public virtual DateTime LastUpdate { get; set; }
    }
}
