using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace Psub.DataAccess
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory ?? (_sessionFactory = Fluently.Configure()
                                                                     .Database(MsSqlConfiguration.MsSql2008.ShowSql()
                                                                     .ConnectionString(c => c.FromConnectionStringWithKey("Base")))
                                                                     .Mappings(x => x.FluentMappings.AddFromAssembly(System.Reflection.Assembly.Load("psub.DataAccess"))
                                                                            .Conventions.Add(PrimaryKey.Name.Is(key => key.Name))
                                                                            .Conventions.Add(ForeignKey.EndsWith("Id")))
                                                                     .ExposeConfiguration(config => config.SetProperty(NHibernate.Cfg.Environment.CurrentSessionContextClass, "web"))
                    //.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))

                                                                     .BuildSessionFactory());
            }
        }
        public static ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(SessionFactory))
                CurrentSessionContext.Bind(SessionFactory.OpenSession());
            return SessionFactory.GetCurrentSession();
        }
        public static void DisposeSession()
        {
            var session = SessionFactory.GetCurrentSession();
            session.Close();
            session.Dispose();
        }
        public static void BeginTransaction()
        {
            SessionFactory.GetCurrentSession().BeginTransaction();
        }
        public static void CommitTransaction()
        {
            var session = SessionFactory.GetCurrentSession();
            if (session.Transaction.IsActive)
                session.Transaction.Commit(); ;
        }
        public static void RollBackTransaction()
        {
            var session = SessionFactory.GetCurrentSession();
            if (session.Transaction.IsActive)
                session.Transaction.Rollback(); ;
        }
    }
}
