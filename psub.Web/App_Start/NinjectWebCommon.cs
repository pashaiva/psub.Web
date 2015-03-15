using NHibernate;
using Psub.DataAccess;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.Concrete;
using Psub.Domain.Entities;
using Psub.Shared.Abstract;
using Psub.Shared.Concrete;

[assembly: WebActivator.PreApplicationStartMethod(typeof(psub.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(psub.Web.App_Start.NinjectWebCommon), "Stop")]

namespace psub.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IRepository<User>>().To<Repository<User>>();
            kernel.Bind<IRepository<Publication>>().To<Repository<Publication>>();
            kernel.Bind<IRepository<ControlObject>>().To<Repository<ControlObject>>();
            kernel.Bind<IRepository<DataParameter>>().To<Repository<DataParameter>>();
            kernel.Bind<IRepository<RelayData>>().To<Repository<RelayData>>();
            kernel.Bind<IRepository<FullDataParameter>>().To<Repository<FullDataParameter>>();
            kernel.Bind<IRepository<ActionLog>>().To<Repository<ActionLog>>();
            kernel.Bind<IRepository<PublicationSection>>().To<Repository<PublicationSection>>();
            kernel.Bind<IRepository<PublicationMainSection>>().To<Repository<PublicationMainSection>>();
            kernel.Bind<IRepository<Statistic>>().To<Repository<Statistic>>();

            kernel.Bind<ISession>().ToMethod(x => NHibernateHelper.GetCurrentSession());

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IFileService>().To<FileService>();
            kernel.Bind<IPublicationService>().To<PublicationService>();
            kernel.Bind<IControlObjectService>().To<ControlObjectService>();
            kernel.Bind<IDataParameterService>().To<DataParameterService>();
            kernel.Bind<IRelayDataService>().To<RelayDataService>();
            kernel.Bind<IDrawingService>().To<DrawingService>();
            kernel.Bind<IActionLogService>().To<ActionLogService>();
            kernel.Bind<IPublicationSectionService>().To<PublicationSectionService>();
            kernel.Bind<IPublicationMainSectionService>().To<PublicationMainSectionService>();
            kernel.Bind<IStatisticService>().To<StatisticService>();
        }
    }
}
