using System.ComponentModel.DataAnnotations;
using Psub.Shared;

namespace Psub.DataService.CommonViewModels
{
    public class FormAddressLetterViewModel
    {
        [Display(Name = LanguageConstants.PostForAddress)]
        public string PostForAddress { get; set; }

        [Display(Name = LanguageConstants.NameForAddress)]
        public string NameForAddress { get; set; }
    }
}
