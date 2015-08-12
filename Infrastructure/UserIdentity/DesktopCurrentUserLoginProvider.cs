using System;

namespace Infrastructure.UserIdentity
{
    public class DesktopCurrentUserLoginProvider : ICurrentUserLoginProvider
    {
        public string CurrentUserLogin 
        {
            get { return Environment.UserName; } 
        }
    }
}
