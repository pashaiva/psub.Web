using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using psub.net.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Entities
{
    public class MainSectionCreateGetQuery : IQuery<MainSectionCreateGetViewModel>
    {
        public string Name { get; set; }
    }

    public class MainSectionCreateGetViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = LanguageConstants.Name)]
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }
    }
}
