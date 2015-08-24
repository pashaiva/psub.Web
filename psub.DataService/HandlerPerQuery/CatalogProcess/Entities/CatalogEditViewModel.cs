using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.DataService.CommonViewModels;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Entities
{
    public class CatalogEditPostQuery : CatalogViewModel, IQuery<CatalogEditViewModel>
    {
    }

    public class CatalogEditGetQuery : IQuery<CatalogEditViewModel>
    {
        public int Id { get; set; }
    }

    public class CatalogEditViewModel : CatalogViewModel, IQuery<CatalogEditResult>
    {
    }

    public class CatalogViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

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
        [UIHint("DropDownSelector")]
        public DropDownSelectorViewModel Section { get; set; }
    }

    public class CatalogEditResult : QueryResult
    {
        public int Id { get; set; }
    }
}
