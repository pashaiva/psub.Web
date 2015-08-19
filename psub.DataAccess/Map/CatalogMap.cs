using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class CatalogMap : ClassMap<Catalog>
    {
        public CatalogMap()
        {
            Table("Catalogs");
            Id(m => m.Id);
            Map(m => m.TitleText).Column("Title").Length(500).Not.Nullable();
            Map(m => m.Text).CustomType("StringClob").Not.Nullable();
            Map(m => m.TextPreview).CustomType("StringClob").Not.Nullable();
            Map(m => m.Keywords).Length(300).Nullable();
            Map(m => m.UserName).Length(200).Not.Nullable();
            Map(m => m.UserGuid).Length(100).Not.Nullable();
            Map(m => m.Created).Column("CareteDate");
            Map(m => m.IsPublic);

            References(m => m.Section).Column("CatalogSectionId");
        }
    }
}
