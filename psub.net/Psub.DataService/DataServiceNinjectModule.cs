using NHibernate;
using Ninject.Modules;
using Psub.DataService.Abstract;
using Psub.DataService.Concrete;
using Psub.DataService.HandlerPerQuery;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using Psub.DataAccess.Abstract;
using Psub.Domain.Entities;
using Psub.DataAccess;
using Ninject.Extensions.Conventions;
using psub.net.Shared.Abstract;
using psub.net.Shared.Concrete;

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

            Bind<IRepository<Catalog>>().To<Repository<Catalog>>();
            Bind<IRepository<CatalogComment>>().To<Repository<CatalogComment>>();
            Bind<IRepository<CatalogLike>>().To<Repository<CatalogLike>>();
            Bind<IRepository<CatalogCommentLike>>().To<Repository<CatalogCommentLike>>();
            Bind<IRepository<CatalogSection>>().To<Repository<CatalogSection>>();
            Bind<IRepository<CatalogMainSection>>().To<Repository<CatalogMainSection>>();

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
            Bind<ICatalogSectionService>().To<CatalogSectionService>();
            Bind<ICatalogMainSectionService>().To<CatalogMainSectionService>();

            Bind<ICatalogCommentService>().To<CatalogCommentService>();

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
