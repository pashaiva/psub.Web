using System;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class DataParameter : Base
    {
        public virtual string Value { get; set; }
        public virtual int MeteringType { get; set; }
        public virtual ControlObject ControlObject { get; set; }
        public virtual DateTime LastUpdate { get; set; }
    }
}
