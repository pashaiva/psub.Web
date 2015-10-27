using System;
using System.ComponentModel.DataAnnotations;

namespace Psub.DataService.DTO
{
    public class UserDTO
    {
        public virtual int Id { get; set; }

        [Display(Name = "ФИО")]
        [Required(ErrorMessage = "Необходимо ввести ФИО")]
        public virtual string Name { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Необходимо ввести логин")]
        public virtual string NickName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Необходимо ввести e-mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "E-mail некорректен!")]
        public virtual string Email { get; set; }

        [Display(Name = "Город")]
        public virtual string City { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [StringLength(100, ErrorMessage = "{0} должен быть не менее {2}-и символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [StringLength(100, ErrorMessage = "{0} должен быть не менее {2}-и символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public virtual string ConfirmPassword { get; set; }

        [Display(Name = "Команда")]
        public virtual int TeamId { get; set; }

        public virtual string LastUrl { get; set; }

        [Display(Name = "Дата регистрации")]
        public virtual DateTime DateRegistration { get; set; }

        public string UserGuid { get; set; }
    }
}
