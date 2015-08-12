namespace Psub.DataService.CommonViewModels
{
    public class GeoobjContractItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContractItemName { get; set; }
        public virtual string Prefix { get; set; }
        public int Type { get; set; }
        public int NominalVoltage { get; set; }

        public ContractViewModel Contract { get; set; }
    }
}
