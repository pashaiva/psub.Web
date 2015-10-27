using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.ActionLogProcess.Entities
{
    public class LastActionsHistoryOfSectionListQuery : IQuery<LastActionsHistoryOfSection>
    {
        public string Object { get; set; }
    }

    public class LastActionsHistoryOfSectionListViewModel : ListQuery, IQuery<LastActionsHistoryOfSection>
    {
    }

    public class LastActionsHistoryOfSection : PagingListQueryResult<LastActionsHistoryOfSectionItem>
    {
    }

    public class LastActionsHistoryOfSectionItem
    {
        public string Text { get; set; }
    }
}
