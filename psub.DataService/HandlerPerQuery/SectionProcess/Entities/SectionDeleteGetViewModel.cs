using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Entities
{
    public class SectionDeleteGetQuery : IQuery<SectionDeleteGetViewModel>
    {
        public int Id { get; set; }
    }

    public class SectionDeleteGetViewModel
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
