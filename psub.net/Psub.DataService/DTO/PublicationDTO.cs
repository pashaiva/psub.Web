using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.Domain.Entities;

namespace Psub.DataService.DTO
{
    public class PublicationDTO
    {
        public int Id { get; set; }

        [Display(Name = "Автор")]
        public string Name { get; set; }
        public string UserGuid { get; set; }


        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название статьи")]
        public string Title { get; set; }

        [AllowHtml]
        [Display(Name = "Содержание")]
        [Required(ErrorMessage = "Введите текст статьи")]
        public string Text { get; set; }

        [Display(Name = "Дата добавления")]
        public string CreateDateString { get { return string.Format("{0} {1}", CreateDate.ToShortDateString(), CreateDate.ToShortTimeString()); } }
        public DateTime CreateDate { get; set; }

        [Display(Name = "Дата редактирования")]
        public string EditDateString
        {
            get
            {
                return string.Format("{0} {1}", EditDate.HasValue ? EditDate.Value.ToShortDateString() : "",
                                     EditDate.HasValue ? EditDate.Value.ToShortTimeString() : "");
            }
        }
        public DateTime? EditDate { get; set; }

        [Display(Name = "Подменю")]
        [Required(AllowEmptyStrings = true)]
        public List<PublicationSectionDTO> PublicationSection { get; set; }
        [Display(Name = "Меню")]
        [Required(AllowEmptyStrings = true)]
        public List<PublicationMainSectionDTO> PublicationMainSection { get; set; }

        [Display(Name = "Подменю")]
        [Required(AllowEmptyStrings = true)]
        public int PublicationSectionId { get; set; }
        [Display(Name = "Раздел:")]
        public string PublicationSectionName { get; set; }
        [Display(Name = "Меню")]
        [Required(AllowEmptyStrings = true)]
        public int PublicationMainSectionId { get; set; }
        [Display(Name = "Раздел:")]
        public string PublicationMainSectionName { get; set; }

        [Display(Name = "Ключевые слова:")]
        public string Keywords { get; set; }

        public string Guid { get; set; }
        [Display(Name = "Прикрепленные файлы:")]
        public List<File> Files { get; set; }
    }
}
