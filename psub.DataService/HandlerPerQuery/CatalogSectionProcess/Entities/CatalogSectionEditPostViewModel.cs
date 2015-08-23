using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities
{
    public class CatalogSectionEditPostQuery : IQuery<CatalogSectionEditPostViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MainSectionId { get; set; }
    }

    public class CatalogSectionEditPostViewModel
    {
        public int Id { get; set; }
    }
}
