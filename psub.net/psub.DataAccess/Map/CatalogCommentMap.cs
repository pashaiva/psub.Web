﻿using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class CatalogCommentMap : ClassMap<CatalogComment>
    {
        public CatalogCommentMap()
        {
            Id(m => m.Id);
            Map(m => m.Text).CustomType("StringClob").Not.Nullable();
            Map(m => m.UserName).Length(200).Not.Nullable();
            Map(m => m.UserGuid).Length(100).Not.Nullable();
            Map(m => m.Created);
            Map(m => m.Guid);

            References(m => m.AnswerTo).Nullable();

            References(m => m.Catalog);

            HasMany(x => x.Likes).Fetch.Join().Inverse().Cascade.SaveUpdate();
            HasMany(x => x.Replys).KeyColumn("AnswerToId").KeyNullable();
        }
    }
}