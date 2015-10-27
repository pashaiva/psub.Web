using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities
{
    public class CatalogSectionDeleteGetQuery : IQuery<CatalogSectionDeleteGetViewModel>
    {
        public int Id { get; set; }
    }

    public class CatalogSectionDeleteGetViewModel
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
