namespace Psub.DataService.DTO
{
    public class PublicationSectionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PublicationMainSectionDTO PublicationMainSection { get; set; }
    }
}
