using System.Collections.Generic;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class PublicationMainSection : Base
    {
       public virtual IList<PublicationSection> PublicationSections { get; set; }
    }
}
