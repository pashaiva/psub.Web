using System.Linq;
using System.Web;
using Psub.DataAccess.Abstract;
using Psub.DataService.HandlerPerQuery.ActionLogProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.ActionLogProcess.Handlers
{
    public class LastActionsHistoryOfSectionListHandler : IQueryHandler<LastActionsHistoryOfSectionListQuery, LastActionsHistoryOfSection>
    {
        private readonly IRepository<ActionLog> _actionLogRepository;

        public LastActionsHistoryOfSectionListHandler(IRepository<ActionLog> actionLogRepository)
        {
            _actionLogRepository = actionLogRepository;
        }

        public LastActionsHistoryOfSection Handle(LastActionsHistoryOfSectionListQuery catalog)
        {
            var url = HttpContext.Current.Request.Url;
           
            return new LastActionsHistoryOfSection
                {
                    Items = _actionLogRepository.Query()
                                .Where(m => m.Type.Contains(catalog.Object)) //&& (DateTime.Now.Date - m.Date.Date).Days < 10)
                                .OrderByDescending(m1 => m1.Id)
                                .Take(100)
                                .ToList()
                                .Select(m2 => new LastActionsHistoryOfSectionItem
                                    {
                                        Text = string.Format("{0} {1} {2} {3}",
                                        m2.Date,
                                        m2.UserName,
                                        m2.ActionName,
                                        !string.IsNullOrEmpty(m2.ObjectName) 
                                        ? m2.ObjectName 
                                        : string.Format("<a href='{0}://{1}:{2}{3}'>{4}</a>",
                                        url.Scheme,
                                        url.Host,
                                        url.Port,
                                        VirtualPathUtility.ToAbsolute(string.Format("~/{0}/{1}/{2}", m2.Type, "Details", m2.ObjectId)),
                                        !string.IsNullOrEmpty(m2.ObjectName) ? m2.ObjectName : "подробнее>>>")
                                       )
                                    }).ToList()
                };
        }
    }
}
