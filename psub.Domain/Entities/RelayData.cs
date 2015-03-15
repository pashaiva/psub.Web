using System;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class RelayData : Base
    {
        public virtual int PinName { get; set; }
        public virtual bool Value { get; set; }
        public virtual ControlObject ControlObject { get; set; }
        public virtual DateTime LastUpdate { get; set; }
    }
}
