using System.Collections.Generic;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class ControlObject : Base
    {
        public virtual string Discription { get; set; }
        public virtual string Guid { get; set; }
        public virtual Client Client { get; set; }
        public virtual IList<DataParameter> DataParameters { get; set; }
        public virtual IList<RelayData> RelayDatas { get; set; } 
    }
}
