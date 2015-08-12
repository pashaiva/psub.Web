using System.Collections.Generic;

namespace Psub.DataService.CommonViewModels
{
    public class DropDownSelectorViewModel
    {
        public int Id { get; set; }
        public List<DropDownItem> Items { get; set; }
    }

    public class DropDownItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
