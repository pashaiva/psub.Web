using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class Like : Base
    {
        public virtual string ObjectType { get; set; }
        public virtual int ObjectId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UserGuid { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual bool IsLike { get; set; }
        public virtual PublicationComment Comment { get; set; }
    }
}
