using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class PublicationMap : ClassMap<Publication>
    {
        public PublicationMap()
        {
            Table("Publications");
            Id(m => m.Id);
            Map(m => m.Name).Column("UserName");
            Map(m => m.UserGuid);
            Map(m => m.Title).Not.Nullable();
            Map(m => m.Text).CustomType("StringClob").Not.Nullable();
            Map(m => m.CreateDate).Column("CareteDate");
            Map(m => m.EditDate);
            Map(m => m.Keywords);
            Map(m => m.Guid);

            References(m => m.PublicationMainSection).Column("PublicationMainSectionId");
            References(m => m.PublicationSection).Column("PublicationSectionId");
        }
    }
}
