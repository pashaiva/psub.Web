using System.Linq;

namespace Psub.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        int SaveOrUpdate(T instance);
        void Delete(int id);
        T Get(int id);
        IQueryable<T> Query();
        string EntityName { get; }
    }
}
