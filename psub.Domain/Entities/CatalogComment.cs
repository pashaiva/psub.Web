using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class CatalogComment : Base
    {
        public virtual string Text { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UserGuid { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual CatalogComment AnswerTo { get; set; }
        public virtual IList<CatalogComment> Replys { get; set; }
        public virtual string Guid { get; set; }

        public virtual IList<CatalogLike> Likes { get; set; }
    }
}
