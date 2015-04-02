using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Psub.DTO.Entities
{
    public class ClientDTO
    {
        public  int Id { get; set; }
        public  string Guid { get; set; }
        [Display(Name = "Клиент")]
        public  string Name { get; set; }
        [Display(Name = "Описанеи")]
        public  string Discription { get; set; }
        [Display(Name = "Сотрудники")]
        public  IList<UserDTO> Users { get; set; }
    }
}
