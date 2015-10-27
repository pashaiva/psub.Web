using System;
using System.ComponentModel.DataAnnotations;
using psub.net.Shared;

namespace Psub.DataService.CommonViewModels
{
    public class SetUserViewModel
    {
        public string UserName { get; set; }
        public string UserGuid { get; set; }
        public DateTime? Date { get; set; }
        public bool IsConfirm { get; set; }
        [Display(Name = LanguageConstants.AddInfo)]
        public string AddInfo { get; set; }

        public int ObjectId { get; set; }
        public string ObjectType { get; set; }

        public bool MakeEdit { get; set; }
    }
}
