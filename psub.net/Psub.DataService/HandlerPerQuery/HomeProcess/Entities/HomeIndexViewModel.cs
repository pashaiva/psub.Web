using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;

namespace Psub.DataService.HandlerPerQuery.HomeProcess.Entities
{
    public class HomeIndexQuery : IQuery<HomeIndexViewModel>
    {
    }

    public class HomeIndexViewModel
    {
        public System.Collections.Generic.IList<PublicationListItem> Publications { get; set; }
        public System.Collections.Generic.IList<CatalogListItem> Catalogs { get; set; }
    }
}
