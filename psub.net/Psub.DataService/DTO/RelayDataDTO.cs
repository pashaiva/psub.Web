using System.ComponentModel.DataAnnotations;

namespace Psub.DataService.DTO
{
    public class RelayDataDTO
    {
        public int Id { get; set; }
        [Display(Name = "Назначение")]
        public string Name { get; set; }
        [Display(Name = "Вывод (pin)")]
        public int PinName { get; set; }
        [Display(Name = "вкл/откл")]
        public bool Value { get; set; }
        public ControlObjectDTO ControlObject { get; set; }
        [Display(Name = "Параметр обнавлен")]
        public string LastUpdate { get; set; }
    }
}
