﻿using FluentNHibernate.Mapping;
using Psub.Domain.Entities;

namespace Psub.DataAccess.Map
{
    public class PublicationSectionMap : ClassMap<PublicationSection>
    {
        public PublicationSectionMap()
        {
            Table("PublicationSection");
            Id(m => m.Id).Column("Id");
            Map(m => m.Name).Column("Name");

            References(m => m.PublicationMainSection).Column("PublicationMainSectionId");
        }
    }
}
