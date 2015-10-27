using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities
{
    public class CatalogMainSectionCreatePostQuery : IQuery<CatalogMainSectionCreatePostViewModel>
    {
        public string Name { get; set; }
    }

    public class CatalogMainSectionCreatePostViewModel
    {
        public int Id { get; set; }
    }
}
