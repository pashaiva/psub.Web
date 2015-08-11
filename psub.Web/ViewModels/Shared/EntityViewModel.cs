using Psub.Domain.Entities;

namespace psub.Web.ViewModels.Shared
{
    public class EntityViewModel
    {
        public EntityTypes Type { get; set; }
        public int Id { get; set; }
        public bool ShowTasks { get; set; }
        public bool ShowHistory { get; set; }

        public EntityViewModel(EntityTypes type, int id, bool showTasks, bool showHistory)
        {
            Type = type;
            Id = id;
            ShowTasks = showTasks;
            ShowHistory = showHistory;
        }
    }
}