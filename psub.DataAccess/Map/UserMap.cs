using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).Column("Id");
            Map(x => x.Name).Column("UserName");
            Map(x => x.NickName).Column("NickName");
            Map(x => x.Password).Column("Password");
            Map(x => x.DateRegistration).Column("DateRegistration");
            Map(x => x.LastUrl).Column("LastUrl");
            Map(x => x.Email).Column("Email");
            Map(x => x.City).Column("City");
            Map(x => x.UserGuid).Column("UserGuid");
            References(x => x.Client).Column("ClientId");
        }
    }
}
