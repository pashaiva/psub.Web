using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class PublicationComment : Base
    {
        public virtual string Text { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UserGuid { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual PublicationComment AnswerTo { get; set; }
        public virtual string Guid { get; set; }

        public virtual IList<Like> Likes { get; set; }
    }
}
