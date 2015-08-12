using System;

namespace Psub.DTO.Entities
{
    public class ActionLogDTO
    {
        public virtual int Id { get; set; }
        public virtual string ActionName { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UserGuid { get; set; }
        public virtual DateTime Date { get; set; }
        public string DateJson { get; set; }
        public virtual string Type { get; set; }
        public virtual int ObjectId { get; set; }
        public virtual string ObjectName { get; set; }
    }
}
