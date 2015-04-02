using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Psub.DTO.Entities
{
    public class ControlObjectDTO
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Описанеи")]
        public string Discription { get; set; }
        [Display(Name = "Клиент")]
        public ClientDTO Client { get; set; }
        [Display(Name = "GUID")]
        public string Guid { get; set; }
        [Display(Name = "Контроллируемые параметры")]
        public List<DataParameterDTO> DataParameters { get; set; }
        [Display(Name = "Контроллируемые параметры")]
        public List<RelayDataDTO> RelayData { get; set; }
    }
}
