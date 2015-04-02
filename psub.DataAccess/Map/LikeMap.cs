using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    internal class LikeMap : ClassMap<Like>
    {
        public LikeMap()
        {
            Id(m => m.Id);
            Map(m => m.ObjectType).ReadOnly();
            Map(m => m.ObjectId);
            Map(m => m.UserName).Length(200).Not.Nullable();
            Map(m => m.UserGuid).Length(100).Not.Nullable();
            Map(m => m.Created);
            Map(m => m.IsLike);
            References(x => x.Comment).Column("PublicationCommentId");
        }
    }
}
