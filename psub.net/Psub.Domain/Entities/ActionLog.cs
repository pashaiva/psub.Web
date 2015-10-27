using System;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class ActionLog : Base
    {
        public virtual string ActionName { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UserGuid { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Type { get; set; }
        public virtual int ObjectId { get; set; }
        public override int Id { get; set; }
        public override string Name { get; set; }
        public virtual string ObjectName { get; set; }
    }
}
