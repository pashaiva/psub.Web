using System;
using System.Web;
using psub.net.Shared.Extensions;

namespace psub.net.Shared
{
    public static class FormatUtlities
    {
        public static string FormatGravatarImageLink(string email)
        {
            return String.Format("http://www.gravatar.com/avatar/{0}?d={1}", email.HashEmailForGravatar(), "http://media.tumblr.com/tumblr_lak5phfeXz1qzqijq.png");
        }

        public static string FormatVirtualDetailsUrl(string objectName, int id, string urlName = "перейти >>>", string hash = "")
        {
            try
            {
                return string.Format("<a href=\"{0}\">{1}</a>", VirtualPathUtility.ToAbsolute(string.Format("~/{0}/{1}/{2}#{3}", objectName, "Details", id, hash)), urlName);
            }
            catch (Exception)
            {
                return string.Format("<a href=\"{0}\">{1}</a>", string.Format("~/{0}/{1}/{2}#{3}", objectName, "Details", id, hash), urlName);
            }
        }

        public static string FormatCreated(string name, DateTime date)
        {
            return (string.IsNullOrWhiteSpace(name) || date == DateTime.MinValue) ? LanguageConstants.NoData : string.Format("{0}, {1}", name, date);
        }
    }
}
