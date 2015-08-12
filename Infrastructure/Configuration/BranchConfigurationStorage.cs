using System.Collections.Generic;
using System.Configuration;

namespace Infrastructure.Configuration
{
    public static class BranchConfigurationStorage
    {
        private const string SectionName = "branches";

        static BranchConfigurationStorage()
        {
            var section = (BranchesConfigSection)ConfigurationManager.GetSection(SectionName);

            CurrentBranch = section.Server.Current;

            BranchServers = new Dictionary<string, BranchServerElement>();
            foreach (BranchServerElement branch in section.Server)
            {
                BranchServers[branch.Name] = branch;
            }

            BranchClients = new Dictionary<string, BranchClientElement>();
            foreach (BranchClientElement branch in section.Client)
            {
                BranchClients[branch.Name] = branch;
            }
        }

        public static readonly string CurrentBranch;

        public static readonly Dictionary<string, BranchServerElement> BranchServers;

        public static readonly Dictionary<string, BranchClientElement> BranchClients;
    }
}
