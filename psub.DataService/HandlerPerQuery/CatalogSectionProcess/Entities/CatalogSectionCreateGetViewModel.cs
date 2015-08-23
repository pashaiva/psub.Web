using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities
{
    public class CatalogSectionCreateGetQuery : IQuery<CatalogSectionCreateGetViewModel>
    {
        public int Id { get; set; }
    }

    public class CatalogSectionCreateGetViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = LanguageConstants.Name)]
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }

        [Display(Name = LanguageConstants.MainSection)]
        public int MainSectionId { get; set; }

        public List<CatalogMainSectionItem> MainSections { get; set; }
    }
}
