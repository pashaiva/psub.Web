using System.Configuration;

namespace Infrastructure.Configuration
{
    public static class Configurations
    {
        public static int CacheExpirationInMinutes
        {
            get { return GetIntValue("", 24*60); }
        }

        #region active directory groups

        /// <summary>
        /// todo
        /// </summary>
        public static string PhotoAdminGroup { get { return GetStringValue("photoAdminADGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string InfoMaterialAdminGroup { get { return GetStringValue("infoMaterialsAdminADGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string BanGroup { get { return GetStringValue("banGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string OfficeGroup { get { return GetStringValue("officeADGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string DivisionChiefsGroup { get { return GetStringValue("divisionChiefsADGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string ChiefGroup { get { return GetStringValue("chiefADGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string FinancialGroup { get { return GetStringValue("financialADGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string ContractReadersGroup { get { return GetStringValue("readContractsADGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string GipGroup { get { return GetStringValue("gipADGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string EditorGroup { get { return GetStringValue("EditGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string ViewerGroup { get { return GetStringValue("ViewGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string DocFlowFullAccessGroup { get { return GetStringValue("docFlowFullAccessGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string DocFlowLimitedAccessGroup { get { return GetStringValue("docFlowLimitedAccessGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string DispatchGroup { get { return GetStringValue("dispatchGroupADGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string GiperAdminGroup { get { return GetStringValue("giperAdminGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string TemplatesAdministrator { get { return GetStringValue("templatesAdministratorsADGroup"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string SiteInputDocCreatorAdmin { get { return GetStringValue("siteInputDocCreatorAdmin"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string SiteOutputDocCreator { get { return GetStringValue("siteOutputDocCreator"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string SiteOutputDocCreatorAdmin { get { return GetStringValue("siteOutputDocCreatorAdmin"); } }

        /// <summary>
        /// todo Могут выгружать файлы документов(писем)
        /// </summary>
        public static string CanDownloadDocFile { get { return GetStringValue("canDownloadDocFile"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string ContractAdministrator { get { return GetStringValue("contractAdministrator"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string IsMakeAddSitesToDocs { get { return GetStringValue("MakeAddSitesToDocs"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string ServiceJobUsers { get { return GetStringValue("ServiceJobUsers"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string RequestNotificationUser { get { return GetStringValue("RequestNotificationUser"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string AdminDepartmentSubstation { get { return GetStringValue("AdminDepartmentSubstation"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string AdminBuildingDepartment { get { return GetStringValue("AdminBuildingDepartment"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string AdminPublications { get { return GetStringValue("AdminPublications"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string AdminManualBuildingDepartment { get { return GetStringValue("AdminManualBuildingDepartment"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string GroupConfiguration { get { return GetStringValue("groupConfiguration"); } }

        /// <summary>
        /// todo
        /// </summary>
        public static string ElectronicArchive { get { return GetStringValue("electronicArchive"); } }

        #endregion

        #region private

        private static int GetIntValue(string appSettingsKey, int defaultValue)
        {
            return ConfigurationManager.AppSettings[appSettingsKey] == null
                       ? defaultValue
                       : int.Parse(ConfigurationManager.AppSettings[appSettingsKey]);
        }

        public static string GetStringValue(string appSettingsKey, string defaultValue = "")
        {
            return ConfigurationManager.AppSettings[appSettingsKey] ?? defaultValue;
        }

        #endregion
    }
}
