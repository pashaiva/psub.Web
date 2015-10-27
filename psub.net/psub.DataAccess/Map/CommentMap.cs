using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class CommentMap:ClassMap<Comment>
    {
        public CommentMap()
        {
            Id(m => m.Id);
            Map(m => m.Text).CustomType("StringClob");
            Map(m => m.UserName);
            Map(m => m.UserGuid);
            Map(m => m.Created);
            References(m => m.ChildrenComment);
        }
    }
}
