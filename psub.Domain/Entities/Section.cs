using System.Collections.Generic;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class Section : Base
    {
        public virtual MainSection MainSection { get; set; }
    }

    public class MainSection : Base
    {
        public virtual IList<Section> Sections { get; set; }
    }
}
