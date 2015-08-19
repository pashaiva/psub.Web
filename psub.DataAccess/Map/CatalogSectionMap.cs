using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class CatalogSectionMap : ClassMap<CatalogSection>
    {
        public CatalogSectionMap()
        {
            Table("CatalogSection");
            Id(m => m.Id).Column("Id");
            Map(m => m.Name).Column("Name");

            References(m => m.MainSection).Column("CatalogMainSectionId");
        }
    }
}
