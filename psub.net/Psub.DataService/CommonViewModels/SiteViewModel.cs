namespace Psub.DataService.CommonViewModels
{
    public class SiteViewModel
    {
        public int SiteId { get; set; }
        public string SiteTitle { get; set; }
        public string SiteDescription { get; set; }
        public int SiteVersion { get; set; }
        public bool IsTaken { get; set; }

        public bool IsViewSended { get; set; }
        public bool IsSended { get; set; }
    }
    public class CommonSiteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Version { get; set; }
        public bool IsTaken { get; set; }
    }
}
