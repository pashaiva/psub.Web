using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Entities
{
    public class MainSectionCreatePostQuery : IQuery<MainSectionCreatePostViewModel>
    {
        public string Name { get; set; }
    }

    public class MainSectionCreatePostViewModel
    {
        public int Id { get; set; }
    }
}
