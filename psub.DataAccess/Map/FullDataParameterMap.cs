using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class FullDataParameterMap : ClassMap<FullDataParameter>
    {
        public FullDataParameterMap()
        {
            Table("FullDataParameter");
            Id(m => m.Id).Column("Id");
            Map(m => m.Name).Column("Name");
            Map(m => m.Value).Column("Value");
            Map(m => m.LastUpdate).Column("LastUpdate");
            References(m => m.ControlObject).Column("ControlObjectId");
        }
    }
}
