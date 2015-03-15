using System;
using System.Web;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepository<Statistic> _statisticRepository;
        private readonly IUserService _userService;

        public StatisticService(IRepository<Statistic> statisticRepository,
            IUserService userService)
        {
            _statisticRepository = statisticRepository;
            _userService = userService;
        }

        public int Save(string url, string urlReferrer)
        {
            var ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


            if (HttpContext.Current.Request.RequestContext.HttpContext.Request.Url != null)
                return _statisticRepository.SaveOrUpdate(new Statistic
                    {
                        Url = url,
                        UrlReferrer = urlReferrer,
                        UserName = _userService.GetCurrentUser().Name,
                        Date = DateTime.Now,
                        IP = ip
                    });

            return 0;
        }
    }
}
