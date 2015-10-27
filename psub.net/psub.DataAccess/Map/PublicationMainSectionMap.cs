using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class PublicationMainSectionMap : ClassMap<MainSection>
    {
        public PublicationMainSectionMap()
        {
            Table("PublicationMainSection");
            Id(m => m.Id).Column("Id");
            Map(m => m.Name).Column("Name");

            HasMany(m => m.Sections).KeyColumn("PublicationMainSectionId").LazyLoad();
        }
    }
}
