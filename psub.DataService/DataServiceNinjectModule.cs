using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Ninject.Modules;
using Psub.DataAccess;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.Concrete;
using Psub.DataService.HandlerPerQuery;
using Psub.Domain.Entities;
using Psub.Shared.Abstract;
using Psub.Shared.Concrete;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using Ninject.Extensions.Conventions;

namespace Psub.DataService
{
    public class DataServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            #region data services + ptoject service

            Bind<IRepository<User>>().To<Repository<User>>();
            Bind<IRepository<Publication>>().To<Repository<Publication>>();
            Bind<IRepository<ControlObject>>().To<Repository<ControlObject>>();
            Bind<IRepository<DataParameter>>().To<Repository<DataParameter>>();
            Bind<IRepository<RelayData>>().To<Repository<RelayData>>();
            Bind<IRepository<FullDataParameter>>().To<Repository<FullDataParameter>>();
            Bind<IRepository<ActionLog>>().To<Repository<ActionLog>>();
            Bind<IRepository<Section>>().To<Repository<Section>>();
            Bind<IRepository<MainSection>>().To<Repository<MainSection>>();
            Bind<IRepository<Statistic>>().To<Repository<Statistic>>();
            Bind<IRepository<PublicationComment>>().To<Repository<PublicationComment>>();
            Bind<IRepository<Like>>().To<Repository<Like>>();
            Bind<IRepository<PublicationCommentLike>>().To<Repository<PublicationCommentLike>>();

            Bind<ISession>().ToMethod(x => NHibernateHelper.GetCurrentSession());

            Bind<IUserService>().To<UserService>();
            Bind<IFileService>().To<FileService>();
            Bind<IPublicationService>().To<PublicationService>();
            Bind<IControlObjectService>().To<ControlObjectService>();
            Bind<IDataParameterService>().To<DataParameterService>();
            Bind<IRelayDataService>().To<RelayDataService>();
            Bind<IDrawingService>().To<DrawingService>();
            Bind<IActionLogService>().To<ActionLogService>();
            Bind<IPublicationSectionService>().To<PublicationSectionService>();
            Bind<IPublicationMainSectionService>().To<PublicationMainSectionService>();
            Bind<IStatisticService>().To<StatisticService>();
            Bind<IPublicationCommentService>().To<PublicationCommentService>();
            Bind<ILikeService>().To<LikeService>();

            #endregion
            
            #region query handlers

            Bind<IMediator>().To<Mediator>();

            Kernel.Bind(x => x.FromThisAssembly()
                              .SelectAllClasses()
                              .InheritedFrom(typeof(IQueryHandler<,>))
                              .BindAllInterfaces());

            #endregion
        }
    }
}
