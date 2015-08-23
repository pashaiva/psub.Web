using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace Psub.Shared
{
    public static class ConfigData
    {
        public static string GetAppSettingsItem(string name)
        {
            if (ConfigurationManager.AppSettings[name] == null)
                throw new NullReferenceException("В appSettings отсутствует свойство с таким ключом.");
            return ConfigurationManager.AppSettings[name];
        }

        public static void SetAppSettingItem(string name, string value)
        {
            if (ConfigurationManager.AppSettings[name] == null)
                throw new NullReferenceException("В appSettings отсутствует свойство с таким ключом.");
            ConfigurationManager.AppSettings[name] = value;
        }

        public static string ApplicationName
        {
            get
            {
                if (HttpContext.Current.Application["appName"] == null)
                    HttpContext.Current.Application["appName"] = GetAppSettingsItem("appName");
                return (string)HttpContext.Current.Application["appName"];
            }
        }
        public static string MainTitle
        {
            get
            {
                if (HttpContext.Current.Application["mainTitle"] == null)
                    HttpContext.Current.Application["mainTitle"] = GetAppSettingsItem("mainTitle");
                return (string)HttpContext.Current.Application["mainTitle"];
            }
        }
        public static string MainDescription
        {
            get
            {
                if (HttpContext.Current.Application["mainDescription"] == null)
                    HttpContext.Current.Application["mainDescription"] = GetAppSettingsItem("mainDescription");
                return (string)HttpContext.Current.Application["mainDescription"];
            }
        }
        public static string MainKeywords
        {
            get
            {
                if (HttpContext.Current.Application["mainKeywords"] == null)
                    HttpContext.Current.Application["mainKeywords"] =GetAppSettingsItem("mainKeywords");
                return (string)HttpContext.Current.Application["mainKeywords"];
            }
        }

        public static List<string> FileExtensionForOpenInBrowser = new List<string>(new[] { ".pdf", ".jpg", ".png" });

        public static int PageSize = 10;

        public static int FileMaxSize = 10;

        public static string FileDirectory = "WebFiles";
    }
}
