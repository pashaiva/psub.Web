using System.Collections.Generic;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class Client : Base
    {
        public virtual string Guid { get; set; }
        public virtual string Discription { get; set; }
        public virtual IList<User> Users { get; set; }
    }
}
