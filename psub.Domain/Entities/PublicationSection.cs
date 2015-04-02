using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
   public class PublicationSection : Base
    {
       public virtual PublicationMainSection PublicationMainSection { get; set; }
    }
}
