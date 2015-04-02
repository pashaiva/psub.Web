using System;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class User : Base
    {
        public virtual string NickName { get; set; }
        public virtual string Email { get; set; }
        public virtual string City { get; set; }
        public virtual string Password { get; set; }
        public virtual int TeamId { get; set; }
        public virtual string LastUrl { get; set; }
        public virtual DateTime DateRegistration { get; set; }
        public virtual string UserGuid { get; set; }
        public virtual Client Client { get; set; }
    }
}
