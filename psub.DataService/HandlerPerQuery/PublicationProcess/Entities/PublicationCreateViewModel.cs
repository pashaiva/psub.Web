using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.Domain.Entities;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Entities
{
    public class PublicationCreateQuery : IQuery<PublicationCreateResult>
    {
        public string TitleText { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        [AllowHtml]
        public string TextPreview { get; set; }
        public string Keywords { get; set; }
        public bool IsPublic { get; set; }
    }

    public class PublicationCreateViewModel : IQuery<PublicationCreateResult>
    {
        [Display(Name = LanguageConstants.Name)]
        [Required(ErrorMessage = "Введите название")]
        public string TitleText { get; set; }

        [Display(Name = LanguageConstants.Preview)]
        [UIHint("TextEditor")]
        [AllowHtml]
        [Required(ErrorMessage = "Краткое содержание не должно быть пустым")]
        public string TextPreview { get; set; }

        [Display(Name = LanguageConstants.Content)]
        [UIHint("TextEditor")]
        [AllowHtml]
        [Required(ErrorMessage = "Новость не должна быть пустой")]
        public string Text { get; set; }

        [Display(Name = LanguageConstants.KeyWords)]
        public string Keywords { get; set; }

        [Display(Name = LanguageConstants.AccessAll)]
        public bool IsPublic { get; set; }

        public string Guid { get; set; }
        [Display(Name = "Прикрепленные файлы:")]
        public List<File> Files { get; set; }
    }

    public class PublicationCreateResult : QueryResult
    {
        public int Id { get; set; }
    }
}
