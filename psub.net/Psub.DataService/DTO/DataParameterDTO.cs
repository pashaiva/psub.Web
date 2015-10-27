using System.ComponentModel.DataAnnotations;

namespace Psub.DataService.DTO
{
    public class DataParameterDTO
    {
        public int Id { get; set; }
        [Display(Name = "Параметр")]
        public string Name { get; set; }
        [Display(Name = "Значение")]
        public string Value { get; set; }
        [Display(Name = "Размерность")]
        public int MeteringType { get; set; }
        [Display(Name = "Объект")]
        public ControlObjectDTO ControlObject { get; set; }
        [Display(Name = "Параметр обнавлен")]
        public virtual string LastUpdate { get; set; }
    }
}
