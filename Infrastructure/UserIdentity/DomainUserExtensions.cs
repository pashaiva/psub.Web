using Infrastructure.Configuration;

namespace Infrastructure.UserIdentity
{
    public static class DomainUserExtensions
    {
        public static bool IsBan(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.BanGroup);
        }
        public static bool IsGiperAdmin(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.GiperAdminGroup);
        }
        public static bool IsOfficeWorker(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.OfficeGroup) || user.IsGiperAdmin();
        }
        public static bool IsChief(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.ChiefGroup) || user.IsGiperAdmin();
        }
        public static bool IsFinancialWorker(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.FinancialGroup) || user.IsGiperAdmin();
        }
        public static bool IsCashContractsReader(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.ContractReadersGroup) || user.IsGiperAdmin();
        }
        public static bool IsGip(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.GipGroup) || user.IsGiperAdmin();
        }
        public static bool IsPhotoAdmin(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.PhotoAdminGroup) || user.IsGiperAdmin();
        }
        public static bool IsInfoMaterialAdmin(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.InfoMaterialAdminGroup) || user.IsGiperAdmin();
        }
        public static bool IsTemplatesAdministrator(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.TemplatesAdministrator) || user.IsGiperAdmin();
        }
        public static bool IsDivisionChief(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.DivisionChiefsGroup) || user.IsGiperAdmin();
        }
        public static bool IsDocFlowFullAccess(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.DocFlowFullAccessGroup) || user.IsGiperAdmin();
        }
        public static bool IsDocFlowLimitedAccess(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.DocFlowLimitedAccessGroup) || user.IsGiperAdmin();
        }
        public static bool IsTdmsEditor(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.EditorGroup);
        }
        public static bool IsTdmsViewer(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.ViewerGroup) || user.IsTdmsEditor();
        }
        public static bool IsAdminBuildingDepartment(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.AdminBuildingDepartment) || user.IsGiperAdmin();
        }
        public static bool IsAdminDepartmentSubstation(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.AdminDepartmentSubstation) || user.IsGiperAdmin();
        }
        public static bool IsAdminManualBuildingDepartment(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.AdminManualBuildingDepartment) || user.IsGiperAdmin();
        }
        public static bool IsDispatchGroup(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.DispatchGroup) || user.IsGiperAdmin();
        }
        public static bool IsElectronicArchive(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.ElectronicArchive) || user.IsGiperAdmin();
        }
        public static bool IsGroupConfiguration(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.GroupConfiguration) || user.IsGiperAdmin();
        }
        public static bool IsSiteInputDocCreatorAdmin(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.SiteInputDocCreatorAdmin) || user.IsGiperAdmin();
        }
        public static bool IsSiteOutputDocCreator(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.SiteOutputDocCreator) || user.IsGiperAdmin();
        }
        public static bool IsSiteOutputDocCreatorAdmin(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.SiteOutputDocCreatorAdmin) || user.IsGiperAdmin();
        }
        public static bool CanDownloadDocFile(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.CanDownloadDocFile) || user.IsGiperAdmin();
        }
        public static bool IsContractAdministrator(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.ContractAdministrator) || user.IsGiperAdmin();
        }
        public static bool IsMakeAddSitesToDocs(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.IsMakeAddSitesToDocs) || user.IsGiperAdmin();
        }
        public static bool IsPublicationsAdmin(this DomainUser user)
        {
            return user.IsMemberOf(Configurations.AdminPublications) || user.IsGiperAdmin();
        }
        //todo: нужно исправить
        public static bool IsDepartmentSubstationUser(this DomainUser user)
        {
            return user.DivisionGroup.DivisionId == 7;
        }
        //todo: нужно исправить
        public static bool IsBuildingDepartmentUser(this DomainUser user)
        {
            return user.DivisionGroup.DivisionId == 13;
        }
    }
}
