using System.Collections.Generic;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities
{
    public class CatalogMainSectionListQuery : ListQuery, IQuery<ListCatalogMainSection>
    {
        public int SectionId { get; set; }
    }

    public class ListCatalogMainSection : PagingListQueryResult<CatalogMainSectionListItem>
    {
        public bool IsAdminPublications { get; set; }
    }

    public class CatalogMainSectionListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SectionItem> PublicationSections { get; set; }
    }

    public class CatalogSectionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}