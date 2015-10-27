namespace Infrastructure.Web.Paging
{
    public interface IPager
    {
        int PageCount { get; }
        int CurrentPage { get; }
        int StartDisplayedPage { get; }
        int EndDisplayedPage { get; }
        string GetLinkForPage(int pageIndex);
    }
}