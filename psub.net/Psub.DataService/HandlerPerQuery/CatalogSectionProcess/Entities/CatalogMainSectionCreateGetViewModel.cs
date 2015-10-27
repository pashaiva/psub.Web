using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using psub.net.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities
{
    public class CatalogMainSectionCreateGetQuery : IQuery<CatalogMainSectionCreateGetViewModel>
    {
        public string Name { get; set; }
    }

    public class CatalogMainSectionCreateGetViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = LanguageConstants.Name)]
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }
    }
}
