using System.Collections.Generic;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Entities
{
    public class MainSectionListQuery : ListQuery, IQuery<ListMainSection>
    {
        public int SectionId { get; set; }
    }

    public class ListMainSection : PagingListQueryResult<MainSectionListItem>
    {
        public bool IsAdminPublications { get; set; }
    }

    public class MainSectionListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SectionItem> Sections { get; set; }
    }

    public class SectionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}