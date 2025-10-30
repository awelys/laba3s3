using System.Collections.Generic;
namespace laba1s3core
{
    public interface IRepository<T> where T : IDomainObject
    {
        void Add(T item);
        bool Remove(int id);
        T? Get(int id);
        IEnumerable<T> GetAll();
        bool Update(T item);
    }
}
