using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class PublicationMainSectionMap : ClassMap<PublicationMainSection>
    {
        public PublicationMainSectionMap()
        {
            Table("PublicationMainSection");
            Id(m => m.Id).Column("Id");
            Map(m => m.Name).Column("Name");

            HasMany(m => m.PublicationSections).KeyColumn("PublicationMainSectionId").LazyLoad();
        }
    }
}
