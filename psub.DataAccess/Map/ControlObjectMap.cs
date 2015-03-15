using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class ControlObjectMap : ClassMap<ControlObject>
    {
        public ControlObjectMap()
        {
            Table("ControlObject");
            Id(m => m.Id).Column("Id");
            Map(m => m.Name).Column("Name");
            Map(m => m.Guid).Column("Guid");
            Map(m => m.Discription).Column("Discription");
            References(m => m.Client).Column("Client");
            HasMany(m => m.DataParameters).KeyColumn("ControlObjectId");
            HasMany(m => m.RelayDatas).KeyColumn("ControlObjectId");
        }
    }
}
