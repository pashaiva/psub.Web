using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Entities
{
    public class SectionEditPostQuery : IQuery<SectionEditPostViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MainSectionId { get; set; }
    }

    public class SectionEditPostViewModel
    {
        public int Id { get; set; }
    }
}
