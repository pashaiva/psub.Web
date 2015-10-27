using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using psub.net.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Entities
{
    public class SectionCreateGetQuery : IQuery<SectionCreateGetViewModel>
    {
        public int Id { get; set; }
    }

    public class SectionCreateGetViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = LanguageConstants.Name)]
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }

        [Display(Name = LanguageConstants.MainSection)]
        public int MainSectionId { get; set; }

        public List<MainSectionItem> MainSections { get; set; }
    }
}
