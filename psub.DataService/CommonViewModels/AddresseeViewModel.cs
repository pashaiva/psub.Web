using System;

namespace Psub.DataService.CommonViewModels
{
    public class AddresseeViewModel
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public bool IsTake { get; set; }
        public DateTime? TakeDate { get; set; }
        public string UserName { get; set; }
        public string UserGuid { get; set; }
        public string RejectionInfo { get; set; }
    }
}
