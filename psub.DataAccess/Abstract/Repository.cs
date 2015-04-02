using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Psub.Domain.Abstract;

namespace Psub.DataAccess.Abstract
{
    public class Repository<T> : IRepository<T> where T : class, ISimple
    {
        protected ISession Nhsession;
        public Repository(ISession session)
        {
            Nhsession = session;
        }
        public int SaveOrUpdate(T instance)
        {
            Nhsession.Save(instance);
            return instance.Id;
        }
        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity != null)
                Nhsession.Delete(entity);
        }
        public T Get(int id)
        {
            return id < 0 ? null : Nhsession.Get<T>(id);
        }
        public IQueryable<T> Query()
        {
            return Nhsession.Query<T>();
        }

        public string EntityName { get { return typeof(T).Name; } }

    }
}
