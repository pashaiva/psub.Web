using System.Collections.Generic;

namespace Psub.DataService.DTO
{
    public class PublicationMainSectionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PublicationSectionDTO> PublicationSection { get; set; }
    }
}
