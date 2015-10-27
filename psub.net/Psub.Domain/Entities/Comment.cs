using System;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class Comment : Base
    {
        public virtual string Text { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UserGuid { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual Comment ChildrenComment { get; set; }
        public virtual int ObjectId { get; set; }
        public virtual string ObjectName { get; set; }
    }
}
