using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
  public  class ClientMap:ClassMap<Client>
    {
      public ClientMap()
      {
          Table("Client");
          Id(x => x.Id).Column("Id");
          Map(x => x.Name).Column("Name");
          Map(x => x.Discription).Column("Discription");
          Map(x => x.Guid).Column("Guid");
          HasMany(m => m.Users).KeyColumn("ClientId");
      }
    }
}
