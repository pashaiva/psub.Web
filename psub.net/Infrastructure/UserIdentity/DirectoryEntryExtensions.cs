using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Infrastructure.Extensions;

namespace Infrastructure.UserIdentity
{
    internal static class DirectoryEntryExtensions
    {

        public static string GetProperty(this DirectoryEntry directoryEntry, String property)
        {
            if (directoryEntry == null || !directoryEntry.Properties.Contains(property))
            {
                return string.Empty;
            }
            return directoryEntry.Properties[property].Value.ToString();
        }

        public static string GetPost(this DirectoryEntry directoryEntry)
        {
            return directoryEntry.GetProperty("title");
        }

        public static string GetPhone(this DirectoryEntry directoryEntry)
        {
            return directoryEntry.GetProperty("ipPhone");
        }

        public static string GetOffice(this DirectoryEntry directoryEntry)
        {
            return directoryEntry.GetProperty("physicalDeliveryOfficeName");
        }

        public static IEnumerable<int> GetSubdivisionIdentifiers(this DirectoryEntry directoryEntry)
        {
            return directoryEntry.GetProperty("extensionAttribute3").GetIntList(';');
        }

        public static int GetDivisionId(this DirectoryEntry directoryEntry)
        {
            int divisionId;
            int.TryParse(directoryEntry.GetProperty("extensionAttribute2"), out divisionId);
            return divisionId;
        }
    }
}
