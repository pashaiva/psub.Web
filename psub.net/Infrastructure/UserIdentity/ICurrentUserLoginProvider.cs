namespace Infrastructure.UserIdentity
{
    public interface ICurrentUserLoginProvider
    {
        string CurrentUserLogin { get; }
    }
}
