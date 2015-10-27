using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using psub.net.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Entities
{
    public class CatalogCreateQuery : CatalogCreateGetViewModel, IQuery<CatalogCreateResult>
    {
    }

    public class CatalogCreateViewModel : IQuery<CatalogCreateResult>
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

        [Display(Name = LanguageConstants.Price)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = LanguageConstants.KeyWords)]
        public string Keywords { get; set; }

        [Display(Name = LanguageConstants.AccessAll)]
        public bool IsPublic { get; set; }

        [Display(Name = LanguageConstants.Section)]
        public int SectionId { get; set; }

        public List<MainSectionItem> MainSections { get; set; }
    }

    public class CatalogCreateResult : QueryResult
    {
        public int Id { get; set; }
    }
}
