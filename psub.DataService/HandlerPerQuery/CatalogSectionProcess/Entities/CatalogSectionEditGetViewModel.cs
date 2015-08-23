using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogSectionProcess.Entities
{
    public class CatalogSectionEditGetQuery : IQuery<CatalogSectionEditGetViewModel>
    {
        public int Id { get; set; }
    }

    public class CatalogSectionEditGetViewModel
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

    public class CatalogMainSectionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
