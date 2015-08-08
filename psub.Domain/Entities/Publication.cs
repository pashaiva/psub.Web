using System;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class Publication : Base
    {
        public virtual string UserGuid { get; set; }
        public virtual string Title { get; set; }
        public virtual string Text { get; set; }
        public virtual string TextPreview { get; set; }
        public virtual string Keywords { get; set; }
        public virtual string Guid { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual PublicationSection PublicationSection { get; set; }
        public virtual PublicationMainSection PublicationMainSection { get; set; }

    }
}
