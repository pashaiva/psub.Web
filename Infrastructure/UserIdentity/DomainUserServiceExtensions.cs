using System.Collections.Generic;
using System.Linq;
using Infrastructure.Configuration;

namespace Infrastructure.UserIdentity
{
    public static class DomainUserServiceExtensions
    {
        public static IEnumerable<DomainUser> GetServiceJobUsers(this IDomainUserService service)
        {
            return service.GetGroupMembers(Configurations.ServiceJobUsers, recursive: true);
        }
        public static IEnumerable<DomainUser> GetGipUsers(this IDomainUserService service)
        {
            return service.GetGroupMembers(Configurations.GipGroup);
        }
        public static IEnumerable<DomainUser> GetChiefUsers(this IDomainUserService service)
        {
            return service.GetGroupMembers(Configurations.ChiefGroup);
        }
        public static IEnumerable<DomainUser> GetDivisionChiefUsers(this IDomainUserService service, bool loadGroups = false)
        {
            return service.GetGroupMembers(Configurations.DivisionChiefsGroup, loadGroups: loadGroups);
        }
        public static IEnumerable<DomainUser> GetRequestUsers(this IDomainUserService service)
        {
            return service.GetGroupMembers(Configurations.RequestNotificationUser);
        }
        public static IEnumerable<DomainUser> GetDivisionChiefUsersForUser(this IDomainUserService service, DomainUser user)
        {
            int targetDivisionId = user.DivisionGroup!=null ? user.DivisionGroup.DivisionId : 0;
            if (targetDivisionId <= 0)
            {
                return Enumerable.Empty<DomainUser>();
            }

            return service.GetDivisionChiefUsers(true)
                .Where(ch => ch.DivisionGroup != null && ch.DivisionGroup.DivisionId == user.DivisionGroup.DivisionId);
        }
        public static IEnumerable<DomainUser> GetDivisionChiefUsersByDivisionId(this IDomainUserService service, int divisionId)
        {
            if (divisionId <= 0)
            {
                return Enumerable.Empty<DomainUser>();
            }
            return service.GetDivisionChiefUsers(true)
                    .Where(ch => ch.DivisionGroup!= null && ch.DivisionGroup.DivisionId == divisionId)
                    .ToList();
        }
    }
}
