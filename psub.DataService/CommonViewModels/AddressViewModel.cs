using System.ComponentModel.DataAnnotations;

namespace Psub.DataService.CommonViewModels
{
    public class AddressViewModel
    {
        public int ContragentId { get; set; }
        public string ContragentName { get; set; }
        public string AddressPile { get; set; }
        public string PostForAddress { get; set; }
        public string NameForAddress { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }
        public string Address { get; set; }
    }
}
