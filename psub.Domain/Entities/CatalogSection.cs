using System.Collections.Generic;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class CatalogSection : Base
    {
        public virtual CatalogMainSection MainSection { get; set; }
    }

    public class CatalogMainSection : Base
    {
        public virtual IList<CatalogSection> Sections { get; set; }
    }
}
