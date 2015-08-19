using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class CatalogLikeMap : ClassMap<CatalogLike>
    {
        public CatalogLikeMap()
        {
            Id(m => m.Id);
            Map(m => m.ObjectType).ReadOnly();
            Map(m => m.ObjectId);
            Map(m => m.UserName).Length(200).Not.Nullable();
            Map(m => m.UserGuid).Length(100).Not.Nullable();
            Map(m => m.Created);
            Map(m => m.IsLike);
            DiscriminateSubClassesOnColumn("ObjectType");
        }
    }

    public class CatalogCommentLikeMap : SubclassMap<CatalogCommentLike>
    {
        public CatalogCommentLikeMap()
        {
            DiscriminatorValue("CatalogComment");
            References(x => x.Comment).Column("CatalogCommentId");
        }
    }
}
