using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Cache;

namespace Infrastructure.UserIdentity
{
    public interface IDomainUserService
    {
        IEnumerable<DomainUser> FindUsersByKey(string key);
        IEnumerable<DomainUser> GetGroupMembers(string groupName, bool recursive = false, bool loadGroups = false);
        DomainUser GetCurrentUser();
        DomainUser GetUserByGuid(string guid, bool loadGroups = false);
        DomainUser GetUserByLogin(string userName, bool loadGroups = false);
        List<DomainUser> GetAllUsers();
    }

    public class DomainUserService : IDomainUserService
    {
        private const string UserType = "user";
        private const string GroupMembersCacheKey = "GroupMembers";
        private const string UserCacheKey = "User";

        private readonly ICurrentUserLoginProvider _currentUserLoginProvider;
        private readonly IApplicationCacheProvider _applicationCacheProvider;
        private readonly IUserCacheProvider _userCacheProvider;

        public DomainUserService(ICurrentUserLoginProvider currentUserLoginProvider,
            IApplicationCacheProvider applicationCacheProvider,
            IUserCacheProvider userCacheProvider)
        {
            _currentUserLoginProvider = currentUserLoginProvider;
            _applicationCacheProvider = applicationCacheProvider;
            _userCacheProvider = userCacheProvider;
        }

        /// <summary>
        /// Ищет пользователей по фрагменту имени без групп.
        /// </summary>
        /// <param name="key">Фрагмент имени.</param>
        /// <returns></returns>
        public IEnumerable<DomainUser> FindUsersByKey(string key)
        {
            var users = new List<DomainUser>();
            var ps = new PrincipalSearcher
            {
                QueryFilter = new UserPrincipal(GetPrincipalContext())
                {
                    DisplayName = string.Format("*{0}*", key)
                }
            };
            PrincipalSearchResult<Principal> result = ps.FindAll();
            foreach (Principal principal in result)
            {
                if (principal.StructuralObjectClass.Equals(UserType, StringComparison.InvariantCulture))
                {
                    var userMemberPrincipal = (UserPrincipal)principal;
                    if (!userMemberPrincipal.IsAccountLockedOut())
                    {
                        users.Add(ToDomainUser(userMemberPrincipal));
                    }
                }
            }
            return users.OrderBy(user => user.FullName).ToList();
        }

        /// <summary>
        /// Возвращает членов группы.
        /// </summary>
        /// <param name="groupName">Имя группы.</param>
        /// <param name="recursive">Если recursive = true, то проверка на членство - рекурсивная.</param>
        /// <param name="loadGroups">
        /// Если loadGroups = true, то будут загружены группы, в которых они состоят непосредственно.
        /// Так как отдел вычисляется на основе групп, то, чтобы получить пользователей с идентификатором отдела, loadGroups должно быть true.
        /// </param>
        /// <returns></returns>
        public IEnumerable<DomainUser> GetGroupMembers(string groupName, bool recursive = false, bool loadGroups = false)
        {
            string cacheKey = GetGroupMembersCacheKey(groupName, recursive, loadGroups);

            var membersFromCache = _applicationCacheProvider.Get<IEnumerable<DomainUser>>(cacheKey);
            if (membersFromCache != null)
            {
                return membersFromCache;
            }

            var members = new List<DomainUser>();

            GroupPrincipal group = GetGroup(IdentityType.Name, groupName);
            PrincipalSearchResult<Principal> memberPrincipals = group.GetMembers(recursive);

            var fillUserTasks = memberPrincipals
                .Where(mp => mp.StructuralObjectClass.Equals(UserType, StringComparison.InvariantCulture))
                .Cast<UserPrincipal>()
                .Where(mp => !mp.IsAccountLockedOut())
                .Select(mp => Task.Factory.StartNew(() => loadGroups ? FillDomainUserWithGroups(mp, false) : ToDomainUser(mp))).ToArray();
            Task.WaitAll(fillUserTasks);

            members.AddRange(fillUserTasks.Select(x => x.Result));
            var membersFinalCollection = members.OrderBy(u => u.FullName).ToList();

            _applicationCacheProvider.Put(cacheKey, membersFinalCollection);
            return membersFinalCollection;
        }

        /// <summary>
        /// Возвращает текущего пользователя c группами, в которых он состоит непосредственно, и на уровень выше.
        /// Требует реализации интерфейса ICurrentUserLoginProvider.
        /// </summary>
        public DomainUser GetCurrentUser()
        {
            return GetUserByLogin(_currentUserLoginProvider.CurrentUserLogin, true);
        }

        /// <summary>
        /// Возвращает пользователя по его Guid.
        /// </summary>
        /// <param name="userGuid">Guid пользователя.</param>
        /// <param name="loadGroups">Если loadGroups = true, то у пользователя будут указаны группы, в которых он состоит непосредственно.</param>
        /// <returns></returns>
        public DomainUser GetUserByGuid(string userGuid, bool loadGroups = false)
        {
            string cacheKey = GetUserCacheKey(userGuid, loadGroups);

            var userFromCache = _userCacheProvider.Get<DomainUser>(cacheKey);
            if (userFromCache != null)
            {
                return userFromCache;
            }

            UserPrincipal principal = GetUser(IdentityType.Guid, userGuid);
            var finalUser = loadGroups ? FillDomainUserWithGroups(principal, false) : ToDomainUser(principal);

            _userCacheProvider.Put(cacheKey, finalUser);
            return finalUser;
        }

        /// <summary>
        /// Возвращает пользователя по его Guid.
        /// Этот метод нужно использовать только для текущего пользователя, так как во всех остальных случаях мы используем Guid. 
        /// Но он уже используется внутри метода GetCurrentUser, поэтому его напрямую можно использовать только для тестовых целей.
        /// </summary>
        /// <param name="userName">Логин пользователя.</param>
        /// <param name="loadGroups">Если loadGroups = true, то у пользователя будут указаны группы, в которых он состоит непосредственно, и на уровень выше.</param>
        /// <returns></returns>
        public DomainUser GetUserByLogin(string userName, bool loadGroups = false)
        {
            string cacheKey = GetUserCacheKey(userName, loadGroups);

            var userFromCache = _userCacheProvider.Get<DomainUser>(cacheKey);
            if (userFromCache != null)
            {
                return userFromCache;
            }

            UserPrincipal principal = GetUser(IdentityType.SamAccountName, userName) ??
                                      GetUser(IdentityType.Name, userName);
            var finalUser = FillDomainUserWithGroups(principal, loadGroups);

            _userCacheProvider.Put(cacheKey, finalUser);
            return finalUser;
        }

        #region private methods

        private static string GetGroupMembersCacheKey(string groupName, bool recursive, bool loadGroups)
        {
            return string.Format("{0}_{1}_{2}_{3}", GroupMembersCacheKey, groupName, recursive, loadGroups);
        }

        private static string GetUserCacheKey(string identity, bool loadGroups)
        {
            return string.Format("{0}_{1}_{2}", UserCacheKey, identity, loadGroups);
        }

        private DomainUser FillDomainUserWithGroups(UserPrincipal userPrincipal, bool loadSupergroups)
        {
            var user = ToDomainUser(userPrincipal);
            if (!user.IsValid)
            {
                return user;
            }

            user.Groups = loadSupergroups ?
                GetGroupsWithSupergroupsForUser(userPrincipal) :
                GetGroupsForUser(userPrincipal);
            return user;
        }

        private IEnumerable<DomainGroup> GetGroupsForUser(UserPrincipal userPrincipal)
        {
            var groups = new List<DomainGroup>();
            var principalGroups = userPrincipal.GetGroups();
            foreach (var principalGroup in principalGroups)
            {
                groups.Add(ToDomainGroup(principalGroup));
            }
            return groups.Distinct().ToList();
        }

        private IEnumerable<DomainGroup> GetGroupsWithSupergroupsForUser(UserPrincipal userPrincipal)
        {
            var directGroups = userPrincipal.GetGroups();

            var supergroups = GetSupergroups(userPrincipal.GetGroups());
            var groups = directGroups.Select(ToDomainGroup).ToList();

            groups.AddRange(supergroups);

            return groups.Distinct().ToList();
        }

        private List<DomainGroup> GetSupergroups(IEnumerable<Principal> directGroups)
        {
            var supergroups = new List<DomainGroup>();
            foreach (var principalGroup in directGroups)
            {
                var underlyingGroups = principalGroup.GetGroups();
                foreach (var underlyingGroup in underlyingGroups)
                {
                    supergroups.Add(ToDomainGroup(underlyingGroup));
                }
            }
            return supergroups;
        }

        private static PrincipalContext GetPrincipalContext()
        {
            return new PrincipalContext(ContextType.Domain);
        }

        private static UserPrincipal GetUser(IdentityType identityType, string identityValue)
        {
            var res = UserPrincipal.FindByIdentity(GetPrincipalContext(), identityType, identityValue);
            return res;
        }

        private static GroupPrincipal GetGroup(IdentityType identityType, string identityValue)
        {
            return GroupPrincipal.FindByIdentity(GetPrincipalContext(), identityType, identityValue);
        }

        private static DomainUser ToDomainUser(UserPrincipal principal)
        {
            if (principal == null)
            {
                return new DomainUser();
            }
            var directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;
            return new DomainUser
            {
                Login = principal.SamAccountName,
                FullName = principal.DisplayName,
                Guid = principal.Guid.ToString(),
                Email = principal.EmailAddress,
                Phone = directoryEntry.GetPhone(),
                PhoneAdditional = principal.VoiceTelephoneNumber,
                PhysicalDeliveryOfficeName = directoryEntry.GetOffice(),
                Post = directoryEntry.GetPost(),
                SubdivisionIdentifiers = directoryEntry.GetSubdivisionIdentifiers()
            };
        }

        private static DomainGroup ToDomainGroup(Principal principal)
        {
            try
            {
                var directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;
                return new DomainGroup
                {
                    Name = principal.Name,
                    Guid = principal.Guid.ToString(),
                    DivisionId = directoryEntry.GetDivisionId(),
                    Description = principal.Description
                };
            }
            catch (System.Exception)
            {
                return new DomainGroup();
            }
        }

        //очень долго, около 30 мин
        public List<DomainUser> GetAllUsers()
        {
            var result = new List<DomainUser>();
            using (var searcher = new PrincipalSearcher(new UserPrincipal(GetPrincipalContext())))
            {
                foreach (var user in searcher.FindAll())
                {
                    var de = user.GetUnderlyingObject() as DirectoryEntry;

                    if (de != null)
                        result.Add(GetUserByLogin((string)de.Properties["sAMAccountName"].Value));
                }
            }

            return result;
        }

        #endregion
    }
}
