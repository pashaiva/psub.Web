namespace Psub.DataService.CommonViewModels
{
    public class SquaredGeoobjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string OldName { get; set; }
        public int NominalVoltage { get; set; }

        public EnergosystemViewModel Energosystem { get; set; }
    }
}
