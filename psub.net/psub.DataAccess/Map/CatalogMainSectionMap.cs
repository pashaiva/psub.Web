using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class CatalogMainSectionMap : ClassMap<CatalogMainSection>
    {
        public CatalogMainSectionMap()
        {
            Table("CatalogMainSection");
            Id(m => m.Id).Column("Id");
            Map(m => m.Name).Column("Name");

            HasMany(m => m.Sections).KeyColumn("CatalogMainSectionId").LazyLoad();
        }
    }
}
