using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Infrastructure.UserIdentity;

namespace Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string GetShortName(this string fullName)
        {
            try
            {
                if (string.IsNullOrEmpty(fullName))
                {
                    return string.Empty;
                }

                string[] splitFullName = fullName.RemoveMultipleSpaces().Split(' ');
                if (splitFullName.Length != 3)
                {
                    return fullName;
                }

                return string.Format("{0} {1}. {2}.", splitFullName[0], splitFullName[1][0], splitFullName[2][0]);
            }
            catch (System.Exception)
            {
                return fullName;
            }
        }

        public static string GetShortIO(this string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return string.Empty;
            }

            string[] splitFullName = fullName.RemoveMultipleSpaces().Split(' ');
            if (splitFullName.Length != 3)
            {
                return fullName;
            }

            return string.Format("{1} {2}", splitFullName[0], splitFullName[1], splitFullName[2]);
        }

        public static string RemoveMultipleSpaces(this string input)
        {
            var regex = new Regex(@"[ ]{2,}", RegexOptions.None);
            input = regex.Replace(input, @" ");
            return input;
        }

        public static List<int> GetIntList(this string input, char separator)
        {
            var list = new List<int>();
            if (!string.IsNullOrEmpty(input))
            {
                input.Split(separator).ToList().ForEach(splittedItem =>
                {
                    int tmpValue;
                    if (int.TryParse(splittedItem.Trim(), out tmpValue))
                    {
                        list.Add(tmpValue);
                    }
                });
            }
            return list;
        }

        public static string RemoveDomainPrefix(this string input)
        {
            return input.Substring(input.IndexOf('\\') + 1);
        }

        public static IEnumerable<string> SmartSplit(this string input, char separator = ';')
        {
            return input.Split(separator).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
        }

        public static string GetShortUserNameAndLogin(this DomainUser user)
        {
            return string.Format("{0} ({1})", user.FullName, user.Login);
        }
    }
}
