using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class DataParameterMap : ClassMap<DataParameter>
    {
        public DataParameterMap()
        {
            Table("DataParameter");
            Id(m => m.Id).Column("Id");
            Map(m => m.Name).Column("Name");
            Map(m => m.Value).Column("Value");
            Map(m => m.MeteringType).Column("MeteringType");
            Map(m => m.LastUpdate).Column("LastUpdate");
            References(m => m.ControlObject).Column("ControlObjectId");
        }
    }
}
