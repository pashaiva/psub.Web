using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
   public class ActionLogMap : ClassMap<ActionLog>
    {
        public ActionLogMap()
        {
            Table("ActionsLog");
            Id(m => m.Id).Column("id");
            Map(m => m.ActionName).Column("action");
            Map(m => m.UserName).Column("userName");
            Map(m => m.UserGuid).Column("userGuid");
            Map(m => m.Date).Column("date");
            Map(m => m.Type).Column("type");
            Map(m => m.ObjectId).Column("objectId");
        }
    }
}
