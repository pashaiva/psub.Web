using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Psub.Shared.Extensions;

namespace Psub.Shared
{
    public static class FormatUtlities
    {
        public static string FormatGravatarImageLink(string email)
        {
            return String.Format("http://www.gravatar.com/avatar/{0}?d={1}", email.HashEmailForGravatar(), "http://media.tumblr.com/tumblr_lak5phfeXz1qzqijq.png");
        }
    }
}
