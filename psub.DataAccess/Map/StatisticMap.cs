using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class StatisticMap : ClassMap<Statistic>
    {
        public StatisticMap()
        {
            Table("Statistic");
            Id(m => m.Id);
            Map(m => m.UserName).Nullable();
            Map(m => m.Url).Nullable();
            Map(m => m.UrlReferrer).Nullable();
            Map(m => m.Date).Nullable();
            Map(m => m.IP).Nullable();
        }
    }
}
