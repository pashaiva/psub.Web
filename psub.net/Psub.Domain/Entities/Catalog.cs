using System;
using System.Collections.Generic;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class Catalog : Base
    {
        public virtual string CaseName { get { return "документом"; } }

        public virtual string TitleText { get; set; }
        public virtual string Text { get; set; }
        public virtual string TextPreview { get; set; }
        public virtual string Keywords { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UserGuid { get; set; }
        public virtual decimal Price { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual IList<PublicationComment> PublicationComment { get; set; }
        public virtual bool IsPublic { get; set; }

        public virtual Section Section { get; set; }
        
        public virtual string GetRegName()
        {
            return TitleText;
        }

    }
}
