using System.Collections.Generic;
using System.Linq;
using Infrastructure.Extensions;

namespace Infrastructure.UserIdentity
{
    public class DomainUser
    {
        public DomainUser()
        {
            Groups = Enumerable.Empty<DomainGroup>();
        }

        public string Login { get; set; }

        public string FullName { get; set; }

        public string ShortName { get { return FullName.GetShortName(); } }

        public string Guid { get; set; }

        public string Email { get; set; }

        public string Post { get; set; }

        public string Phone { get; set; }

        public string PhoneAdditional { get; set; }

        public string PhysicalDeliveryOfficeName { get; set; }

        public IEnumerable<DomainGroup> Groups { get; set; }

        public IEnumerable<int> SubdivisionIdentifiers { get; set; }

        public bool IsValid { get { return !string.IsNullOrEmpty(Guid); } }

        private DomainGroup _divisionGroup;
        //public DomainGroup DivisionGroup
        //{
        //    get { return _divisionGroup ?? (_divisionGroup = Groups.Any() ? Groups.FirstOrDefault(x => x.DivisionId > 0) : new DomainGroup()); }
        //}

        public DomainGroup DivisionGroup { get; set; }

        private List<int> _allAvailableDivisionIdentifiers;
        public List<int> AllAvailableDivisionIdentifiers
        {
            get
            {
                if (_allAvailableDivisionIdentifiers == null)
                {
                    _allAvailableDivisionIdentifiers = new List<int> { DivisionGroup.DivisionId };
                    _allAvailableDivisionIdentifiers.AddRange(SubdivisionIdentifiers);
                }
                return _allAvailableDivisionIdentifiers;
            }
        }

        private HashSet<string> _groupNames;
        private HashSet<string> GroupNames
        {
            get { return _groupNames ?? (_groupNames = new HashSet<string>(Groups.Select(x => x.Name))); }
        }

        public bool IsMemberOf(string groupName)
        {
            return GroupNames.Contains(groupName);
        }
    }

    public class DomainGroup
    {
        public string Name { get; set; }

        public string Guid { get; set; }

        public int DivisionId { get; set; }

        public string Description { get; set; }
    }
}
